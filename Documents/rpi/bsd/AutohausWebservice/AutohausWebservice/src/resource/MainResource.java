package resource;

import java.io.IOException;
import java.math.BigDecimal;
import java.security.NoSuchAlgorithmException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.logging.Logger;

import javax.ws.rs.Consumes;
import javax.ws.rs.GET;
import javax.ws.rs.HeaderParam;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Request;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.Response.Status;
import javax.ws.rs.core.UriInfo;

import com.google.gson.Gson;
import com.htlvil.controller.auth.Authentication;
import com.htlvil.controller.database.Database;
import com.htlvil.model.BillsRequest;
import com.htlvil.model.CustomerUserData;
import com.htlvil.model.ReparaturAnfrage;

@Path("/main")
public class MainResource {
	@Context
	UriInfo uriInfo;
	@Context
	Request request;

	@GET
	@Secured
	@Path("standorte")
	@Produces(MediaType.APPLICATION_JSON)
	public Response getStandorte() {
		String result = "";
		try {
			ArrayList<String> standorte = Database.getInstance().getStandorte();
			Logger.getLogger("MAINRESOURCE").info(standorte.size() + "");
			result = new Gson().toJson(standorte);
		} catch (SQLException e) {
			return Response.status(Status.INTERNAL_SERVER_ERROR).build();
		}

		return Response.ok(result, MediaType.APPLICATION_JSON).build();

	}
	
	
	@POST
	@Secured
	@Path("termin")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response createTermin(String body) {
		String termin ="";
		ReparaturAnfrage ra = new Gson().fromJson(body, ReparaturAnfrage.class);
		
		String msg = ra.getCustomMessage() + " - " + Arrays.toString(ra.getLeistungen().toArray());
		try {
			termin = Database.getInstance().findTermin(ra.getDatesToCheck(),ra.getStandort());
			BigDecimal id = Database.getInstance().insertRechnung(ra.getUser().getUsername(), termin);
			Database.getInstance().insertKundRechHilfe(id,termin,"Villach", msg,ra.getUser().getUsername());
		
		
		} catch (SQLException e) {
			termin = e.getMessage();
			
			Logger.getLogger("MainResource").info(Arrays.toString(e.getStackTrace()));
		}
		
		
		
		return Response.ok(new Gson().toJson(termin), MediaType.APPLICATION_JSON).build();
	}
	

	@GET
	@Secured
	@Path("angebote/{standort}")
	@Produces(MediaType.APPLICATION_JSON)
	public Response getAngebote( @PathParam("standort") String param) {
		String result = "";
		try {
			ArrayList<String> angebote = Database.getInstance().getAngebote(param);
			Logger.getLogger("MAINRESOURCE").info(angebote.size() + "");
			result = new Gson().toJson(angebote);
		} catch (SQLException e) {
			return Response.status(Status.INTERNAL_SERVER_ERROR).build();
		}

		return Response.ok(result, MediaType.APPLICATION_JSON).build();

	}

	@POST
	@Secured
	@Path("bills")
	@Consumes(MediaType.APPLICATION_JSON)
	@Produces(MediaType.APPLICATION_JSON)
	public Response changePassword(String body) {
		String result = "";
		BillsRequest br = new Gson().fromJson(body, BillsRequest.class);

		try {

			ArrayList<byte[]> bytes = Database.getInstance().getBills(br);
			result = new Gson().toJson(bytes);

		} catch (SQLException | IOException e) {
			return Response.status(Status.INTERNAL_SERVER_ERROR).build();
		}
		return Response.ok(result).build();

	}

}
