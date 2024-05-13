



CREATE TABLE BOOKING(
	BookingId				Int IDENTITY(1,1) NOT NULL,
	[Date]					Date NOT NULL,
	[Time]					Time NOT NULL,
	Price					Int NOT NULL,
	EventType				NVarChar(max) NOT NULL,
	Paymenttype				Bit NOT NULL,
	Comment					NVarChar(max) NOT NULL,
	ArtistId				Int NOT NULL,
	CONSTRAINT FK_ARTIST_ArtistId FOREIGN KEY(ArtistId) REFERENCES ARTIST(ArtistId),
	PRIMARY KEY (BookingId)
)

CREATE TABLE Customer(
	CustomerId				int IDENTITY(1,1) NOT NULL,
	BookingId				Int NOT NULL,
	Name					NVarChar(max) NOT NULL,
	PhoneNumber				NVarChar(max) NOT NULL,
	Email					NVarChar(max) NOT NULL,
	ContactPerson			NVarChar(max) NOT NULL,
	Adress					NVarChar(max) NOT NULL,
	CONSTRAINT FK_Booking_BookingId FOREIGN KEY(BookingId) REFERENCES BOOKING(BookingId),
	
	PRIMARY KEY (CustomerId)
)



CREATE TABLE LOCATION(
	BookingId				Int NOT NULL, 		
	Adress					NVarChar(max) NOT NULL,
	City					NVarChar(max) NOT NULL,
	PostCode				Int NOT NULL,
	Name					NVarChar(max) NOT NULL,
	PhoneNumber				NVarChar(max) NOT NULL,
	
	CONSTRAINT PK_Booking_BookingId PRIMARY KEY(BookingId),
	CONSTRAINT FK_Booking_BookingId_Location FOREIGN KEY(BookingId) REFERENCES BOOKING(BookingId),

)







