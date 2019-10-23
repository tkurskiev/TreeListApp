--comment on column app. is '';

CREATE TABLE app.catalog (
	id serial primary key,
	name character varying(200) NOT NULL,
	description text
);

comment on column app.catalog.id is 'Unique id for the catalog table row, containing company information';
comment on column app.catalog.name is 'Manufacturing company name';
comment on column app.catalog.description is 'Description of the company';

--

CREATE TABLE app.catalog_aggregate (
	id serial primary key,
	name character varying(200) NOT NULL,
	description text,
	url character varying (2500),
	catalog_id integer references app.catalog(id)
);

comment on column app.catalog_aggregate.id is 'Unique id for the catalog_aggregate table row, containing aggregate (or in other words, part type) information';
comment on column app.catalog_aggregate.name is 'Particular aggregate''s name';
comment on column app.catalog_aggregate.description is 'Description of the aggregate';
comment on column app.catalog_aggregate.url is 'Url link for page, containing info about the aggregate';
comment on column app.catalog_aggregate.catalog_id is 'Foreign key to table app.catalog; company id, which produces this aggregate';

--

CREATE TABLE app.catalog_model (
	id serial primary key,
	name character varying(200) NOT NULL,
	description text,
	url character varying(2500),
	catalog_aggregate_id integer references app.catalog_aggregate(id)
);

comment on column app.catalog_model.id is 'Unique id for the catalog_model table row, containing aggregate model (or in other words, part model) information';
comment on column app.catalog_model.name is 'Concrete aggregate model''s name';
comment on column app.catalog_model.description is 'Model''s description';
comment on column app.catalog_model.url is 'Url link for page, containing info about the model, or where it can be bought';
comment on column app.catalog_model.catalog_aggregate_id is 'Foreign key to table app.catalog_aggregate; aggregate id, of which type this model is';

--

CREATE TABLE app.catalog_level (
	id serial primary key,
	parent_id integer,
	name character varying(200) NOT NULL,
	description text
);

comment on column app.catalog_level.id is 'Unique id for the catalog_level table row';
comment on column app.catalog_level.parent_id is 'Catalog item''s parent id, if null - row contains info about company';
comment on column app.catalog_level.name is 'Catalog item''s name';
comment on column app.catalog_level.description is 'Catalog item''s description';
