-- MySQL dump 10.13  Distrib 8.0.22, for Win64 (x86_64)
--
-- Host: localhost    Database: birth_reg
-- ------------------------------------------------------
-- Server version	5.7.14

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `child_data`
--

DROP TABLE IF EXISTS `child_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `child_data` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `birthID` varchar(45) NOT NULL,
  `names` varchar(155) NOT NULL,
  `dob` date NOT NULL,
  `sex` varchar(45) NOT NULL,
  `birthPlace` varchar(45) NOT NULL,
  `birthType` varchar(45) NOT NULL,
  `birthOrder` varchar(45) NOT NULL,
  `staffID` varchar(45) NOT NULL,
  `facilityUID` int(11) NOT NULL,
  `dateCreated` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `birthID_UNIQUE` (`birthID`),
  KEY `staffID_idx` (`staffID`),
  KEY `facUID_idx` (`facilityUID`),
  CONSTRAINT `facUID` FOREIGN KEY (`facilityUID`) REFERENCES `staff_table` (`facilityUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `staffID` FOREIGN KEY (`staffID`) REFERENCES `staff_table` (`staffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=1001 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `child_data`
--

LOCK TABLES `child_data` WRITE;
/*!40000 ALTER TABLE `child_data` DISABLE KEYS */;
INSERT INTO `child_data` VALUES (1,'829X5Z','Drea Jayzi','2021-04-23','Male','Hospital','Single','1','111RJ',453675542,'2021-08-04 18:44:18'),(2,'6QB73B','Black Shawn','2021-05-03','Female','Maternity Home','Multiple','2','111RJ',453675542,'2021-08-04 18:44:18'),(3,'IJI3AH','Brown Jame','2021-05-05','Female','At Home','Multiple','5','111RJ',453675542,'2021-08-04 18:44:18'),(4,'56080248','August Alsina','2021-05-06','Male','Port Harcourt','Single','1','121RJ',233455321,'2021-08-04 18:44:18'),(5,'18005918','Dylan James','2021-05-02','Male','Port Harcourt','Single','1','121RJ',233455321,'2021-08-04 18:44:18'),(6,'1370167','Jeremih Pedro','2021-05-03','Male','Port Harcourt','Single','1','RJ123',54534532,'2021-08-04 18:44:18'),(1000,'3954617','Dey Drea','2021-07-08','Female','Jay','Single','1','RJ123',54534532,'2021-08-04 18:44:18');
/*!40000 ALTER TABLE `child_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `father_data`
--

DROP TABLE IF EXISTS `father_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `father_data` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `childBirthID` varchar(45) NOT NULL,
  `name` varchar(155) NOT NULL,
  `ageAtBirth` int(11) NOT NULL,
  `address` varchar(255) NOT NULL,
  `nationality` varchar(45) NOT NULL,
  `state` varchar(45) NOT NULL,
  `ethnic` varchar(45) NOT NULL,
  `job` varchar(45) NOT NULL,
  `staffID` varchar(45) NOT NULL,
  `facilityUID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `birtID_idx` (`childBirthID`),
  KEY `staffID3_idx` (`staffID`),
  KEY `facUID3_idx` (`facilityUID`),
  CONSTRAINT `birtID` FOREIGN KEY (`childBirthID`) REFERENCES `child_data` (`birthID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `facUID3` FOREIGN KEY (`facilityUID`) REFERENCES `staff_table` (`facilityUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `staffID3` FOREIGN KEY (`staffID`) REFERENCES `staff_table` (`staffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `father_data`
--

LOCK TABLES `father_data` WRITE;
/*!40000 ALTER TABLE `father_data` DISABLE KEYS */;
INSERT INTO `father_data` VALUES (1,'829X5Z','Drea Dylan',30,'Loiusiana, New Orleans, US ','Non Nigeria','Las Vega','Non-Hispanic','Software Engineer','111RJ',453675542),(2,'6QB73B','Black James',35,'LA','Non Nigeria','LA','Non-hispanic','Sailor','111RJ',453675542),(3,'IJI3AH','Brown David',32,'Bonny','Nigerian','Rivers State','Ibani','Driver','111RJ',453675542),(4,'56080248','August Kelvin',32,'Akiama Junction','Nigerian','Rivers','Ibani','NLNG Staff','121RJ',233455321),(5,'18005918','David James',34,'Cable Road','Nigerian','Rivers','Ibani','Engineer','121RJ',233455321),(6,'1370167','Jeremih John',32,'Lagos Bus Stop','Nigerian','Rivers','Ibani','Commercial Drivers','RJ123',54534532),(7,'3954617','Dey Will',44,'Joint','Nigerian','River','tong','Seller','RJ123',54534532);
/*!40000 ALTER TABLE `father_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `informant_data`
--

DROP TABLE IF EXISTS `informant_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `informant_data` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `childBirthID` varchar(45) NOT NULL,
  `relWithChild` varchar(45) DEFAULT NULL,
  `name` varchar(155) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `staffID` varchar(45) NOT NULL,
  `facilityUID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `staffID2_idx` (`staffID`),
  KEY `faci=UID2_idx` (`facilityUID`),
  KEY `birthID3_idx` (`childBirthID`),
  CONSTRAINT `birthID3` FOREIGN KEY (`childBirthID`) REFERENCES `child_data` (`birthID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `facUID2` FOREIGN KEY (`facilityUID`) REFERENCES `staff_table` (`facilityUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `staffID2` FOREIGN KEY (`staffID`) REFERENCES `staff_table` (`staffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `informant_data`
--

LOCK TABLES `informant_data` WRITE;
/*!40000 ALTER TABLE `informant_data` DISABLE KEYS */;
INSERT INTO `informant_data` VALUES (1,'829X5Z','','','','111RJ',453675542),(2,'6QB73B','','','','111RJ',453675542),(3,'IJI3AH','Aunty','Will Mia','Bonny','111RJ',453675542),(4,'56080248','','','','121RJ',233455321),(5,'18005918','','','','121RJ',233455321),(6,'1370167','','','','RJ123',54534532),(7,'3954617','','','','RJ123',54534532);
/*!40000 ALTER TABLE `informant_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mother_data`
--

DROP TABLE IF EXISTS `mother_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mother_data` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `childBirthID` varchar(45) NOT NULL,
  `name` varchar(155) NOT NULL,
  `ageAtBirth` int(11) NOT NULL,
  `address` varchar(255) NOT NULL,
  `status` varchar(45) NOT NULL,
  `nationality` varchar(45) NOT NULL,
  `state` varchar(45) NOT NULL,
  `ethnic` varchar(45) NOT NULL,
  `job` varchar(45) NOT NULL,
  `staffID` varchar(45) NOT NULL,
  `facilityUID` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `birthid_idx` (`childBirthID`),
  KEY `staffID1_idx` (`staffID`),
  KEY `facUID_idx` (`facilityUID`),
  CONSTRAINT `birthid` FOREIGN KEY (`childBirthID`) REFERENCES `child_data` (`birthID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `facUID1` FOREIGN KEY (`facilityUID`) REFERENCES `staff_table` (`facilityUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `staffID1` FOREIGN KEY (`staffID`) REFERENCES `staff_table` (`staffID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mother_data`
--

LOCK TABLES `mother_data` WRITE;
/*!40000 ALTER TABLE `mother_data` DISABLE KEYS */;
INSERT INTO `mother_data` VALUES (1,'829X5Z','Jammie Drea',29,'Loiusiana, New Orleans, US','Married','Non Nigeria','Virginia','Non-Hispanic','Scientist','111RJ',453675542),(2,'6QB73B','Black Mia',32,'LA','Married','Non Nigeria','LA','Non-Hispanic','Trader','111RJ',453675542),(3,'IJI3AH','Brown Lian',27,'Bonny','Widowed','Nigerian','Rivers State','Ibani','Trader','111RJ',453675542),(4,'56080248','August Anita',29,'Akiama Junction','Married','Nigerian','Rivers','Ibani','Shutdown worker','121RJ',233455321),(5,'18005918','Glory James',31,'Cable Road','Married','Nigerian','Rivers','Ibani','Trader','121RJ',233455321),(6,'1370167','Jeremih Lucy',29,'Lagos Bus stop','Married','Nigerian','Rivers','Ibani','Trader','RJ123',54534532),(7,'3954617','Dey Min',33,'Joine','Married','Nigerian','Rivers','tong','Driver','RJ123',54534532);
/*!40000 ALTER TABLE `mother_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `staff_table`
--

DROP TABLE IF EXISTS `staff_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `staff_table` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `staffID` varchar(45) NOT NULL,
  `firstName` varchar(45) NOT NULL,
  `lastName` varchar(45) NOT NULL,
  `facilityUID` int(11) NOT NULL,
  `facilityName` varchar(45) NOT NULL,
  `town` varchar(45) NOT NULL,
  `state` varchar(45) NOT NULL,
  `lga` varchar(45) NOT NULL,
  `passcode` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `staffID_UNIQUE` (`staffID`),
  KEY `facilityUID` (`facilityUID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `staff_table`
--

LOCK TABLES `staff_table` WRITE;
/*!40000 ALTER TABLE `staff_table` DISABLE KEYS */;
INSERT INTO `staff_table` VALUES (1,'111RJ','Kim','Jane',453675542,'','','','','123456'),(3,'111AY','Ayo','Aj',456756342,'','','','','123456'),(4,'121RJ','Rejoice','Richmond',233455321,'REJOICE HEALTH CENTRE','BONNY ISLAND','Rivers','Bonny','123456'),(5,'RJ123','Rejoice','Richmond',54534532,'Rejoice Health Clinic','Bonny Island','Rivers','Bonny','123456');
/*!40000 ALTER TABLE `staff_table` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2021-08-06 11:25:02
