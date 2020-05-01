if object_id('InstrumentPropertyAssoc', 'U') is not null drop table InstrumentPropertyAssoc
if object_id('RenterInstrumentAssoc', 'U') is not null drop table RenterInstrumentAssoc
if object_id('Property', 'U') is not null drop table Property
if object_id('Owner', 'U') is not null drop table Owner
if object_id('Renter', 'U') is not null drop table Renter
if object_id('Instrument', 'U') is not null drop table Instrument
if object_id('Person', 'U') is not null drop table Person
if object_id('Address', 'U') is not null drop table Address


create table Address
(
	address_id int not null,
	line1 varchar(50),
	line2 varchar(50),
	line3 varchar(50),
	city varchar(50) not null,
	state varchar(2) not null,
	zip_code varchar(5) not null,
	zip_route varchar(4) null,
	zip_plus_4 as zip_code + isnull(zip_route, ''),
	constraint cpk_Address 
		primary key clustered (address_id)
)
go

create table Person
(
	person_id int not null,
	title varchar(5),
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	full_name as isnull(title + ' ', '') + first_name + ' ' + last_name,
	mailing_address_id int not null,
	shipping_address_id int,
	forwarding_address_id int,
	constraint cpk_Person 
		primary key clustered (person_id),
	constraint cfk_Person_mailing_address
		foreign key (mailing_address_id)
		references Address(address_id),
	constraint cfk_Person_shipping_address
		foreign key (shipping_address_id)
		references Address(address_id),
	constraint cfk_Person_forwarding_address
		foreign key (forwarding_address_id)
		references Address(address_id)
)
go

create table Property
(
	property_id int not null,
	physical_address_id int not null,
	property_type_code varchar(10) not null,
	constraint cpk_Property 
		primary key clustered (property_id),
	constraint cfk_Property_physical_address
		foreign key (physical_address_id)
		references Address (address_id)
)
go

-- Can represent a Title or a Lease Contract
create table Instrument
(
	instrument_id int not null,
	instrument_type_code varchar(10) not null,
	begin_date datetime not null,
	end_date datetime,
	constraint cpk_Instrument
		primary key clustered (instrument_id)
)
go

-- An instrument can involve more than one property
-- and a property can have more than one instrument.
create table InstrumentPropertyAssoc
(
	instrument_id int not null,
	property_id int not null,
	constraint cpk_InstrumentPropertyAssoc
		primary key clustered (instrument_id, property_id),
	constraint cfk_InstrumentPropertyAssoc_Instrument
		foreign key (instrument_id)
		references Instrument (instrument_id),
	constraint cfk_InstrumentPropertyAssoc_Property
		foreign key (property_id)
		references Property (property_id)
)
go

-- Represents a Person who holds (some percentage interest in) 
-- title to one or more properties.
create table Owner
(
	owner_id int not null,
	ownership_percent numeric(5,2) not null default (100.00),
	constraint cpk_Owner
		primary key clustered (owner_id),
	constraint cfk_Owner_Person
		foreign key (owner_id)
		references Person (person_id)
)
go

-- An owner may hold title to multiple instruments.
-- An instrument may have multiple owners.
create table OwnerInstrumentAssoc
(
	owner_id int not null,
	instrument_id int not null,
	constraint cpk_OwnerInstrumentAssoc
		primary key clustered (owner_id, instrument_id),
	constraint cfk_OwnerInstrumentAssoc_Owner
		foreign key (owner_id)
		references Owner (owner_id),
	constraint cfk_OwnerInstrumentAssoc_Instrument
		foreign key (instrument_id)
		references Instrument (instrument_id)
)
go

-- Represents a Person who is leasing one or more properties.
-- A renter (lessee) may have multiple active leases but may 
-- only currently reside at one of them.
-- That address does not have to be the mailing or shipping
-- address of the Person either.
create table Renter
(
	renter_id int not null,
	residential_address_id int not null,
	lease_amount numeric(8, 2) not null,
	lease_payment_period varchar(10) not null,
	constraint cpk_Renter
		primary key clustered (renter_id),
	constraint cfk_Renter_Person
		foreign key (renter_id)
		references Person (person_id),
	constraint cfk_Renter_residential_address
		foreign key (residential_address_id)
		references Address (address_id)
)
go

create table RenterInstrumentAssoc
(
	renter_id int not null,
	instrument_id int not null,
	constraint cpk_RentalInstrumentAssoc
		primary key clustered (renter_id, instrument_id),
	constraint cfk_RenterInstrumentAssoc_Renter
		foreign key (renter_id)
		references Renter (renter_id),
	constraint cfk_RenterInstrumentAssoc_Instrument
		foreign key (instrument_id)
		references Instrument (instrument_id)
)
go


