package gui;

import javafx.beans.value.ObservableValue;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import model.User;
import webserviceaccess.WebserviceController;

public class WorkingController {

	@FXML
	private TabPane tabPane;
	@FXML
	private Tab tabRechnung;
	@FXML
	private Tab tabReparaturen;
	@FXML
	private Tab tabChange;

	private User currentUser = null;

	public void init(User currentUser) {

		if (currentUser != null) {
			this.currentUser = currentUser;
		}

		tabPane.getSelectionModel().selectedItemProperty()
				.addListener((ObservableValue<? extends Tab> observable, Tab oldValue, Tab newValue) -> {
					try {
						if (newValue == tabRechnung) {

							FXMLLoader loader = new FXMLLoader(
									getClass().getClassLoader().getResource("gui/RechnungForm.fxml"));
							Parent root = loader.load();
							RechnungController r = loader.getController();
							r.init(WebserviceController.getInstance().getCurrentUser());
							newValue.setContent(root);

							System.out.println("Rechnung Tab page");

						} else if (newValue == tabReparaturen) {

							FXMLLoader loader = new FXMLLoader(
									getClass().getClassLoader().getResource("gui/ReparaturForm.fxml"));
							Parent root = loader.load();
							ReparaturController rc = loader.getController();
							rc.init(WebserviceController.getInstance().getCurrentUser());
							newValue.setContent(root);

							System.out.println("REparaturen Tab page");
							// ReparturController.init
						} else if (newValue == tabChange) {
							FXMLLoader loader = new FXMLLoader(
									getClass().getClassLoader().getResource("gui/ChangePWD.fxml"));
							Parent root = loader.load();
							ChangePwdController rc = loader.getController();
							rc.init(WebserviceController.getInstance().getCurrentUser());
							newValue.setContent(root);
						}

					} catch (Exception e) {
						System.out.println(e.getMessage());
					}
				});
	}

	public void selectFirst() {

		tabPane.getSelectionModel().select(tabReparaturen);
		tabPane.getSelectionModel().select(tabRechnung);
	}

}