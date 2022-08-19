CREATE TABLE streets(
	id INT PRIMARY KEY,
	name VARCHAR(50),
	city VARCHAR(50)
);

INSERT INTO streets VALUES(1, 'Viamonte', 'Rosario');
INSERT INTO streets VALUES(2, 'Laprida', 'Victoria');
INSERT INTO streets VALUES(3, 'Oroño', 'Rosario');
INSERT INTO streets VALUES(4, 'Rivadavia', 'Victoria');
INSERT INTO streets VALUES(5, 'Pellegrini', 'Rosario');
INSERT INTO streets VALUES(6, 'Sarmiento', 'Posadas');
INSERT INTO streets VALUES(7, 'San Martin', 'Posadas');

SELECT * FROM streets;
