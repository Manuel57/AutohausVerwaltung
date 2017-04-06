package gui;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import model.GlobalVars;
import model.User;
import webserviceaccess.WebserviceController;

public class ChangePwdController {

	private User currentUser;
	@FXML
	private Label lblOld;
	@FXML
	private Label lblnew;
	@FXML
	private Label lblSecondNew;
	@FXML
	private Label lblError;
	@FXML
	private TextField txtOld;
	@FXML
	private TextField txtNew;
	@FXML
	private TextField txtNewCheck;
	@FXML
	private Button btnChange;

	public void init(User curretnUser) {
		if (curretnUser != null) {
			this.currentUser = curretnUser;
			this.txtOld.textProperty().set(this.currentUser.getPassword());
			this.txtOld.disableProperty().set(true);
			this.lblError.setVisible(false);
		}
	}

	@FXML
	public void btnChange_Click(ActionEvent a) {
		if (!txtNew.getText().isEmpty() && !txtNewCheck.getText().isEmpty()) {
			if(txtNew.getText().equals(txtNewCheck.getText())) {
				this.currentUser.setPasswort(txtNew.getText());
				try {
					WebserviceController.getInstance().changePassword(this.currentUser);
					this.lblError.visibleProperty().set(true);
					this.lblError.setText("Your password has been changed successfully");
					
				} catch (Exception e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
			} else {
				this.lblError.visibleProperty().set(true);
				this.lblError.setText("The two passwords do not match!");
			}

		} else {
			this.lblError.visibleProperty().set(true);
			this.lblError.setText("Please fill both Text fields!");
		}
	}

}
