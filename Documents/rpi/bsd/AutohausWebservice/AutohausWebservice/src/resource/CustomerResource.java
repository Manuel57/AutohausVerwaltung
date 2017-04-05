package resource;

import java.security.NoSuchAlgorithmException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.logging.Logger;

import javax.ws.rs.Consumes;
import javax.ws.rs.HeaderParam;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.HttpHeaders;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Request;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;
import javax.ws.rs.core.UriInfo;

import org.glassfish.jersey.internal.util.Base64;

import com.google.gson.Gson;
import com.htlvil.controller.auth.Authentication;
import com.htlvil.controller.database.Database;
import com.htlvil.model.CustomerUserData;

@Path("/user")
public class CustomerResource {
	@Context
	UriInfo uriInfo;
	@Context
	com.sun.research.ws.wadl.Request request;

	@POST
	@Path("check")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public String checkUserdata(String body) {
		String result = "false";
		CustomerUserData cud = new Gson().fromJson(body, CustomerUserData.class);

		try {
			result = new Gson().toJson(Authentication.isAuthorized(cud.getUsername(), cud.getPassword()));
		} catch (SQLException | NoSuchAlgorithmException e) {
		}
		return result;

	}

	@POST
	@Secured
	@Path("changepw")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response changePassword(String body) {
		String result = "";
		CustomerUserData cud = new Gson().fromJson(body, CustomerUserData.class);

		try {

			Database.getInstance().updatePassword(cud.getUsername(), Authentication.generateSha256(cud.getPassword()));
		} catch (SQLException | NoSuchAlgorithmException e) {
			return Response.status(Status.INTERNAL_SERVER_ERROR).build();
		}
		return Response.ok(result).build();

	}

	@POST	
	@Secured
	@Path("createpasswd")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response createPassword(String body) {
		Response result;
		CustomerUserData cud = new Gson().fromJson(body, CustomerUserData.class);
		try {
			if (!cud.getUsername().equals("SmZ8mz9r54kgb7xc"))
				return Response.status(Status.UNAUTHORIZED).build();
		} catch (Exception e1) {
			return Response.status(Status.INTERNAL_SERVER_ERROR).build();
		}

		try {
			cud.setPassword(Authentication.generateSha256(cud.getPassword()));
			result = Response.ok(new Gson().toJson(cud), MediaType.APPLICATION_JSON).build();
		} catch (NoSuchAlgorithmException e) {
			result = Response.status(javax.ws.rs.core.Response.Status.INTERNAL_SERVER_ERROR).build();
		}

		return result;
	}

}
