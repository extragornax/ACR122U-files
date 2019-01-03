-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.45-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema cinema_data
--

CREATE DATABASE IF NOT EXISTS cinema_data;
USE cinema_data;

--
-- Definition of table `customer_details`
--

DROP TABLE IF EXISTS `customer_details`;
CREATE TABLE `customer_details` (
  `Receipt_Number` int(10) unsigned NOT NULL auto_increment,
  `First_Name` varchar(45) NOT NULL,
  `Last_Name` varchar(45) NOT NULL,
  `Address` varchar(150) NOT NULL,
  `Movie` varchar(45) NOT NULL,
  `Redeemed` tinyint(1) NOT NULL default '0',
  PRIMARY KEY  (`Receipt_Number`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `customer_details`
--

/*!40000 ALTER TABLE `customer_details` DISABLE KEYS */;
/*!40000 ALTER TABLE `customer_details` ENABLE KEYS */;


--
-- Definition of procedure `uSP_Find`
--

DROP PROCEDURE IF EXISTS `uSP_Find`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `uSP_Find`(ReceiptNum INT, OUT Result INT)
BEGIN
  DECLARE CountResult INT;
  SET CountResult = 0;
  SELECT COUNT(*)INTO CountResult FROM `customer_details` WHERE Receipt_Number = ReceiptNum and Redeemed = 0;
  IF CountResult > 0 THEN
      UPDATE customer_details SET Redeemed = 1 WHERE Receipt_Number = ReceiptNum;
      SET Result = 1;
  ELSE SET Result = 0;
  END IF;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `uSP_Insert`
--

DROP PROCEDURE IF EXISTS `uSP_Insert`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `uSP_Insert`(FirstName varchar(45), LastName varchar(45), Addr varchar(150), Movie varchar(45), OUT ReceiptNum integer)
BEGIN
  INSERT INTO customer_details (First_Name, Last_Name, Address, Movie)
  VALUES(FirstName, LastName, Addr, Movie);
  SET ReceiptNum = LAST_INSERT_ID();
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;



/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
