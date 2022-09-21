CREATE DEFINER=`root`@`localhost` PROCEDURE `procedure1`()
BEGIN
/*USE mdb;*/
UPDATE exampoints SET Maths = 56, Russian=76, Social=56 WHERE ID=2;
UPDATE exampoints SET Maths = 2, Russian=12, Social=1 WHERE ID=3;
UPDATE exampoints SET Maths = 1, Russian=98, Social=67 WHERE ID=6;
UPDATE exampoints SET Maths = 67, Russian=68, Social=45 WHERE ID=7;
UPDATE exampoints SET Maths = 99, Russian=12, Social=0 WHERE ID=8;

/*INSERT INTO mdb.exampoints (Maths, Russian, Social, ID) VALUES (56, 76, 56, 2);
INSERT INTO mdb.exampoints (Maths, Russian, Social, ID) VALUES (2, 12, 1, 3);
INSERT INTO mdb.exampoints (Maths, Russian, Social, ID) VALUES (1, 98, 67, 6);
INSERT INTO mdb.exampoints (Maths, Russian, Social, ID) VALUES (67, 68, 45, 7);
INSERT INTO mdb.exampoints (Maths, Russian, Social, ID) VALUES (99, 12, 0, 8);*/
END