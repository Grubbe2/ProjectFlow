
CREATE TABLE ARTIST(
	ArtistId				Int IDENTITY(1,1) NOT NULL,
	SocialSecurityNumber 	Int NOT NULL,
	Name					NVarChar(max) NOT NULL,
	Adress					NVarChar(max) NOT NULL,
	City					NVarChar(max) NOT NULL,
	Email					NVarChar(max) NOT NULL,
	Alias					NVarChar(max) NOT NULL,
	PhoneNumber				NVarChar(max) NOT NULL,
	PRIMARY KEY (ArtistId)

)

CREATE TABLE JOBFUNCTION(
	JobfunctionId			int IDENTITY(1,1) NOT NULL,
	Name					NVarChar(max) NOT NULL,
	Description				NVarChar(max) NOT NULL,
	Activity				Bit NOT NULL,
	
	PRIMARY KEY (JobfunctionId)
)



CREATE TABLE ATTRIBUTE(
	AttributeId 			Int IDENTITY(1,1) NOT NULL,
	Characteristics			NVarChar(max) NOT NULL,
	Services				NVarChar(max) NOT NULL,
	
	PRIMARY KEY (AttributeId)
)







