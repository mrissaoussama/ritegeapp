-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 11, 2022 at 01:23 PM
-- Server version: 10.4.19-MariaDB
-- PHP Version: 8.0.7

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `controleaccessdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `controllers`
--

CREATE TABLE `controllers` (
  `Code` smallint(6) NOT NULL,
  `idnode` smallint(6) NOT NULL,
  `ipadr` char(50) NOT NULL,
  `port` smallint(6) NOT NULL,
  `macadr` varchar(20) NOT NULL,
  `modelName` varchar(20) NOT NULL,
  `idSite` smallint(6) NOT NULL,
  `Stat` int(4) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `door`
--

CREATE TABLE `door` (
  `idDoor` smallint(6) NOT NULL,
  `doorNumber` smallint(6) DEFAULT NULL,
  `doorName` varchar(20) DEFAULT NULL,
  `idControler` smallint(6) DEFAULT NULL,
  `idPorte` smallint(6) DEFAULT NULL,
  `canal` smallint(6) DEFAULT NULL,
  `idSite` smallint(6) DEFAULT NULL,
  `stat` smallint(6) DEFAULT NULL,
  `TypeMateriel` varchar(20) DEFAULT NULL,
  `FonctionMateriel` varchar(20) DEFAULT NULL,
  `hasSlaveReader` tinyint(1) NOT NULL,
  `flow` varchar(10) DEFAULT NULL,
  `activated` tinyint(1) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `event`
--

CREATE TABLE `event` (
  `indexEvent` int(11) NOT NULL,
  `dateEvent` date NOT NULL,
  `HeureEvent` varchar(8) NOT NULL,
  `DoorNumber` smallint(6) NOT NULL,
  `userNumber` smallint(6) DEFAULT NULL,
  `codeEvent` smallint(6) NOT NULL,
  `codeControler` smallint(6) NOT NULL,
  `indiceControler` smallint(6) NOT NULL,
  `selected` tinyint(1) NOT NULL,
  `numAccessCard` varchar(11) DEFAULT NULL,
  `Data12` tinyint(50) DEFAULT NULL,
  `Flux` smallint(6) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `eventtype`
--

CREATE TABLE `eventtype` (
  `codeTypeEvent` smallint(6) NOT NULL,
  `descEvent` varchar(200) NOT NULL,
  `withUser` tinyint(1) NOT NULL DEFAULT 0,
  `typeAcces` varchar(10) DEFAULT NULL,
  `keyTypeEvent` varchar(20) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `usersystem`
--

CREATE TABLE `usersystem` (
  `userCode` smallint(6) NOT NULL,
  `firstName` varchar(25) NOT NULL,
  `lastName` varchar(25) NOT NULL,
  `icn` varchar(8) DEFAULT NULL,
  `tel1` varchar(25) DEFAULT NULL,
  `tel2` varchar(25) DEFAULT NULL,
  `email` varchar(55) DEFAULT NULL,
  `departement` varchar(25) DEFAULT NULL,
  `active` tinyint(1) NOT NULL,
  `accessMode` varchar(25) DEFAULT NULL,
  `siteAccessCard` tinyint(1) DEFAULT NULL,
  `numAccessCard` varchar(11) DEFAULT NULL,
  `startValidateDate` datetime DEFAULT NULL,
  `endValidateDate` datetime DEFAULT NULL,
  `picture` varchar(150) DEFAULT NULL,
  `userApplication` smallint(6) DEFAULT NULL,
  `sync` smallint(1) NOT NULL,
  `Fonction` varchar(25) DEFAULT NULL,
  `Alias` varchar(16) DEFAULT NULL,
  `Adresse` varchar(50) DEFAULT NULL,
  `PinCode` char(4) DEFAULT NULL,
  `ControllerSyncStatus` int(11) DEFAULT NULL,
  `CodeProfil` smallint(6) DEFAULT NULL,
  `ActiveDate` tinyint(1) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `controllers`
--
ALTER TABLE `controllers`
  ADD PRIMARY KEY (`Code`),
  ADD UNIQUE KEY `macadr` (`macadr`);

--
-- Indexes for table `door`
--
ALTER TABLE `door`
  ADD PRIMARY KEY (`idDoor`),
  ADD UNIQUE KEY `doorNumber` (`doorNumber`);

--
-- Indexes for table `event`
--
ALTER TABLE `event`
  ADD PRIMARY KEY (`indexEvent`);

--
-- Indexes for table `eventtype`
--
ALTER TABLE `eventtype`
  ADD PRIMARY KEY (`codeTypeEvent`);

--
-- Indexes for table `usersystem`
--
ALTER TABLE `usersystem`
  ADD PRIMARY KEY (`userCode`),
  ADD KEY `fk_Profil_CodeProfil` (`CodeProfil`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
