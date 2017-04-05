package resource;

import java.io.IOException;
import java.lang.annotation.Retention;

import javax.annotation.Priority;
import javax.security.sasl.AuthenticationException;
import javax.ws.rs.Priorities;
import javax.ws.rs.container.*;
import javax.ws.rs.core.HttpHeaders;
import javax.ws.rs.core.Response;
import javax.ws.rs.ext.Provider;

import com.htlvil.controller.auth.Authentication;

import java.lang.annotation.Target;
import java.security.NoSuchAlgorithmException;
import java.sql.SQLException;

@Secured
@Provider
@Priority(Priorities.AUTHENTICATION)
public class AuthenticationFilter implements ContainerRequestFilter {

	@Override
	public void filter(ContainerRequestContext requestContext) throws IOException {
		try {
			String authHeader = requestContext.getHeaderString(HttpHeaders.AUTHORIZATION);
			if (authHeader == null || !authHeader.startsWith("Basic ")) {

				throw new AuthenticationException("Authorization header must be provided");
			}
			if (!Authentication.isAuthorized(authHeader)) {
				throw new AuthenticationException("Unouthorized");
			}

		} catch (AuthenticationException e) {
			requestContext.abortWith(Response.status(Response.Status.UNAUTHORIZED).build());
		} catch (NoSuchAlgorithmException | SQLException e) {
			requestContext.abortWith(Response.status(Response.Status.INTERNAL_SERVER_ERROR).build());
		}
	}

}
