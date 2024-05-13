CREATE TABLE ARTIST_JOBFUNCTION(
	SocialSecurityNumber 	Int NOT NULL,
	JobfunctionId			Int NOT NULL,
	
	
	CONSTRAINT PK_ArtistJobfuntion_SSNJobfunctionId PRIMARY KEY(SocialSecurityNumber, JobfunctionId),
	CONSTRAINT FK_ArtistJobfuntion_SSN FOREIGN KEY(SocialSecurityNumber) REFERENCES ARTIST(SocialSecurityNumber),
	CONSTRAINT FK_ArtistJobfunction_JobfunctionId FOREIGN KEY(JobfunctionId) REFERENCES JOBFUNCTION(JobfunctionId)
)


CREATE TABLE ARTIST_ATTRIBUTE(
	AttributeId				Int NOT NULL,
	SocialSecurityNumber	Int NOT NULL,
	
	
	CONSTRAINT PK_ArtistAttribute PRIMARY KEY(AttributeId, SocialSecurityNumber),
	CONSTRAINT FK_ArtistAttribute_AttributeId FOREIGN KEY (AttributeId) REFERENCES ATTRIBUTE(AttributeId),
	CONSTRAINT FK_ArtistAttribute_SocialSecurityNumber FOREIGN KEY (SocialSecurityNumber) REFERENCES ARTIST(SocialSecurityNumber)
)