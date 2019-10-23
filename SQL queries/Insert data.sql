INSERT INTO app.catalog(name, description)
	VALUES ('VOLVO', 'VOLVO company.'),
	('ER', 'ER company.');

--

INSERT INTO app.catalog_aggregate(
	name, catalog_id)
	VALUES
			('КПП', 1),
			('Двигатель', 2),
			('КПП', 2);
	
--

INSERT INTO app.catalog_model(
	name, catalog_aggregate_id)
	VALUES
	('A365', 1),
	('M4566', 2),
	('FG4511', 2),
	('T45459', 3);
