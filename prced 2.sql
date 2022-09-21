CREATE DEFINER=`root`@`localhost` PROCEDURE `procedure2`()
BEGIN
/*DELETE FROM mdb.exampoints WHERE (exampoints.Maths + exampoints.Russian + exampoints.Social) > 300;
DELETE FROM mdb.exampoints WHERE (exampoints.Maths + exampoints.Russian + exampoints.Social) < 200;*/
UPDATE exampoints SET Maths = 0, Russian=0, Social=0 WHERE (exampoints.Maths + exampoints.Russian + exampoints.Social) < 200;
UPDATE exampoints SET Maths = 999, Russian=999, Social=999 WHERE (exampoints.Maths + exampoints.Russian + exampoints.Social) > 300;
END