Amido.Testing.Jwt
=================

Library to simplify the creation of JWT tokens

1)  Use through PowerShell

	- Open and build the solution.
	- Navigate to the folder in which the .dll "Amido.Testing.Jwt.dll" is built
	- Import the module into PowerShell with the following command "Import-Module [*PATH TO ASSEMBLY*]\Amido.Testing.Jwt.dll
	- The call the "Get-JwtToken" method on the CmdLet
	
	Examples
	Get-JwtToken -issuer https://myissuer.com/trust -Audience https://my.site.com/trust -Thumbprint ee9c049d3d547fcde9b0c53d0f7c481c9f0b5654 -StoreName My -StoreLocation LocalMachine -Base64Encode True -Claims "{'CustomerId':'12345','Email':'jwt@amido.com'}"
	
2)	Reference as a Class Library.  

	- Pull down via NuGet, call the CreateJwtToken on the TokenFactory class supplying the parameters for the token within the JwtTokenProperties class.

	
	
###Available Parameters

####Mandatory

|Parameter		|	Description														| Example / Default 								   |
|---------------|-------------------------------------------------------------------|------------------------------------------------------|
-Issuer			|	The trusted issuer with which the token will be created with    |
-Audience		|	The trusted audience for which the token will be used with      |
-Thumbprint		|	The thumbprint of the certificate to sign the token with        |
-StoreName		|	The store name of the certificate to sign the token with		|	Possible Values include "My", "Root"
-StoreLocation	|	The store location of the certificate to sign the token with	|	Possible Values include "LocalMachine", "CurrentUser"


####Optional

|Parameter		|	Description														| Example / Default 								   					|
|---------------|-------------------------------------------------------------------|-----------------------------------------------------------------------|
-Base64			|	Base64 encode the resulting token								|	Default is true
-CreatedDate	|	The date in which the token is not valid before					|	Default UtcNow - 24 Hours
-ExpiryDate		|	The date in which the token will expire							|	Default	UtcNow + 24 hours
-Claims			|	A Json Collection of the claims to include within the token		|	Example: -Claims "{'CustomerId':'12345','Email':'jwt@amido.com'}"