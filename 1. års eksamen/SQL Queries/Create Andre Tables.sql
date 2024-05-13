
CREATE TABLE ARTIST_JOBFUNCTION(
	ArtistId			 	Int NOT NULL,
	JobfunctionId			Int NOT NULL,
	
	
	CONSTRAINT PK_ArtistJobfuntion_ArtistIdJobfunctionId PRIMARY KEY(ArtistId, JobfunctionId),
	CONSTRAINT FK_ArtistJobfuntion_ArtistId FOREIGN KEY(ArtistId) REFERENCES ARTIST(ArtistId),
	CONSTRAINT FK_ArtistJobfunction_JobfunctionId FOREIGN KEY(JobfunctionId) REFERENCES JOBFUNCTION(JobfunctionId)
)


CREATE TABLE ARTIST_ATTRIBUTE(
	AttributeId				Int NOT NULL,
	ArtistId				Int NOT NULL,
	
	
	CONSTRAINT PK_ArtistAttribute PRIMARY KEY(AttributeId, ArtistId),
	CONSTRAINT FK_ArtistAttribute_AttributeId FOREIGN KEY (AttributeId) REFERENCES ATTRIBUTE(AttributeId),
	CONSTRAINT FK_ArtistAttribute_ArtistId FOREIGN KEY (ArtistId) REFERENCES ARTIST(ArtistId)
)