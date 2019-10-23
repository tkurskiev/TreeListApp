SELECT c.id, c.name, c.description, ca.name aggregate_name, cm.name model_name
FROM app.catalog c
INNER JOIN app.catalog_aggregate ca ON c.id = ca.catalog_id
INNER JOIN app.catalog_model cm ON ca.id = cm.catalog_aggregate_id;

--

select *
from app.catalog_level
order by id, parent_id nulls first

TRUNCATE app.catalog_level RESTART IDENTITY;

--------------------------------------------------------------------------------------
--------------------------------------------------------------------------------------
--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--
--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--
--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--
--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--
--------------------------------------------------------------------------------------
------------РАБОЧИЙ ВАРИАНТ------------------------------------------------------------
------------РАБОЧИЙ ВАРИАНТ------------------------------------------------------------
--------------------------------------------------------------------------------------

INSERT INTO app.catalog_level(name, description)
SELECT name, description
FROM app.catalog
RETURNING id, name;

WITH aggregatesinsertqte as (
	INSERT INTO app.catalog_level(parent_id, name, description)
	SELECT cl.id, ca.name, ca.description
	FROM app.catalog_aggregate as ca
	INNER JOIN app.catalog as c ON ca.catalog_id = c.id
	INNER JOIN app.catalog_level as cl ON c.name = cl.name
	RETURNING id, name, parent_id
)

--SELECT * FROM aggregatesinsertqte;

INSERT INTO app.catalog_level(parent_id, name, description)
SELECT qte.id, cm.name, cm.description
FROM app.catalog_model as cm
INNER JOIN app.catalog_aggregate as ca ON cm.catalog_aggregate_id = ca.id
INNER JOIN app.catalog as c ON ca.catalog_id = c.id
INNER JOIN aggregatesinsertqte as qte ON ca.name = qte.name --AND qte.parent_id = (SELECT cl.id from app.catalog_level as cl WHERE cl.name = c.name)
WHERE qte.parent_id = (SELECT cl.id from app.catalog_level as cl WHERE cl.name = c.name)

--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++--
--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++--
--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++--
--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++==\./\./==++--++--
--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--
--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--
--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--
--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--\\//--//\\--
--В три отдельных запроса

INSERT INTO app.catalog_level(name, description)
SELECT name, description
FROM app.catalog;

INSERT INTO app.catalog_level(parent_id, name, description)
SELECT cl.id, ca.name, ca.description
FROM app.catalog_aggregate as ca
INNER JOIN app.catalog as c ON ca.catalog_id = c.id
INNER JOIN app.catalog_level as cl ON c.name = cl.name;

INSERT INTO app.catalog_level(parent_id, name, description)
SELECT cl.id, cm.name, cm.description
FROM app.catalog_model as cm
INNER JOIN app.catalog_aggregate as ca ON cm.catalog_aggregate_id = ca.id
INNER JOIN app.catalog as c ON ca.catalog_id = c.id 
INNER JOIN (SELECT * FROM app.catalog_level WHERE parent_id IS NOT NULL) as cl ON cl.name = ca.name
	AND cl.parent_id = (SELECT id FROM app.catalog_level WHERE name = c.name);

