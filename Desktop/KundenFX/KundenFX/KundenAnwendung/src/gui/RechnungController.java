package gui;

import java.awt.Desktop;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.List;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.RadioButton;
import javafx.scene.control.TextField;
import javafx.scene.control.Toggle;
import javafx.scene.control.ToggleGroup;
import model.GlobalVars;
import model.User;
import webserviceaccess.WebserviceController;

public class RechnungController {

	private User currentUser = null;
	private List<byte[]> allBills = null;
	@FXML
	private Button btnShow;

	@FXML
	private Label lblZeitraum;
	@FXML
	private Label lblChoose;
	@FXML
	private Label lblError;
	@FXML
	private TextField txtMoWe;
	@FXML
	private ToggleGroup group = new ToggleGroup();

	@FXML
	private RadioButton rbtnWeek;
	@FXML
	private RadioButton rbtnMonth;
	@FXML
	private RadioButton rbtnHalfYear;

	public void init(User currentUser) {
		if (currentUser != null) {
			this.currentUser = currentUser;
		}
		System.out.println("in rechnungn controller init");
		group.selectedToggleProperty().addListener(new ChangeListener<Toggle>() {
			public void changed(ObservableValue<? extends Toggle> ov, Toggle old_toggle, Toggle new_toggle) {
				if (group.getSelectedToggle() != null) {

					lblError.setText("");
					if (new_toggle == rbtnWeek) {

						txtMoWe.disableProperty().set(false);

					} else if (new_toggle == rbtnMonth) {

						txtMoWe.disableProperty().set(false);

					} else if (new_toggle == rbtnHalfYear) {

						txtMoWe.disableProperty().set(true);

					}
				}

			}

		});
	}

	@FXML
	private void btnShow_Click(ActionEvent av) {
		if (group.getSelectedToggle() != null) {
			Toggle selected = group.getSelectedToggle();
			if (selected == rbtnWeek) {
				if (txtMoWe.textProperty().get() != null && !txtMoWe.textProperty().get().isEmpty()) {
					int num = Integer.parseInt(txtMoWe.getText());
					if (validateMonthWeek(num, GlobalVars.WEEK)) {
						try {
							allBills = WebserviceController.getInstance().getRechnungen(GlobalVars.WEEK, num);
							wirteBillsInFile();
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					} else {
						lblError.setText("Bitte geben sie eine gültige Woche ein");
					}
				} else {
					lblError.setText("Bitte geben sie eine Woche ein!");
				}
			} else if (selected == rbtnMonth) {
				if (txtMoWe.textProperty().get() != null && !txtMoWe.textProperty().get().isEmpty()) {
					int num = Integer.parseInt(txtMoWe.getText());
					if (validateMonthWeek(num, GlobalVars.MONTH)) {
						try {
							allBills = WebserviceController.getInstance().getRechnungen(GlobalVars.MONTH, num);
							wirteBillsInFile();
						} catch (Exception e) {
							// TODO Auto-generated catch block
							e.printStackTrace();
						}
					} else {
						lblError.setText("Bitte geben sie einen gültige Monat ein");
					}
				} else {
					lblError.setText("Bitte geben sie einen Monat ein!");
				}
			} else if (selected == rbtnHalfYear) {
				try {
					allBills = WebserviceController.getInstance().getRechnungen(GlobalVars.HALFYEAR, 0);
					wirteBillsInFile();
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				lblError.setText("HalfYear");
			}
		}
	}

	private void wirteBillsInFile() {
		if (allBills != null) {
			String path = "./Rechnung";
			int i = 1;
			for (byte[] b : allBills) {
				FileOutputStream fos;
				try {
					fos = new FileOutputStream(path + i + ".pdf");

					fos.write(b);
					fos.close();
					i++;
				} catch (Exception e) {
					System.out.println(e.getMessage());
					e.printStackTrace();
				}
			}
//			if (Desktop.isDesktopSupported()) {
//			    try {
//			    	System.out.println("supported");
//					Desktop.getDesktop().open(new File("/home/pupil/workspace/BSD/"));
//				} catch (IOException e) {
//					// TODO Auto-generated catch block
//					e.printStackTrace();
//				}
//			}
		}
	}

	private boolean validateMonthWeek(int number, String einheit) {
		boolean ret = false;
		switch (einheit) {
		case GlobalVars.MONTH:
			if (number >= 1 && number <= 12) {
				ret = true;
			}
			break;
		case GlobalVars.WEEK:
			if (number >= 1 && number <= 52) {
				ret = true;
			}
			break;
		default:

			break;
		}
		return ret;
	}
}
