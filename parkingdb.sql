-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 11, 2022 at 01:00 PM
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
-- Database: `parkingdb`
--

-- --------------------------------------------------------

--
-- Table structure for table `abonne`
--

CREATE TABLE `abonne` (
  `idAbonne` int(11) NOT NULL,
  `activerAbonnement` tinyint(4) NOT NULL,
  `multiEntree` tinyint(4) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `abonnement`
--

CREATE TABLE `abonnement` (
  `idAbonnement` int(11) NOT NULL,
  `nomAbonnement` varchar(100) NOT NULL,
  `periodeAbonnement` varchar(100) NOT NULL,
  `nombrePeriode` int(11) DEFAULT 0,
  `montant` decimal(13,3) NOT NULL,
  `idIntervalle` int(11) DEFAULT NULL,
  `idGroupeTarif` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `abonnement_parking`
--

CREATE TABLE `abonnement_parking` (
  `idParking` int(11) NOT NULL,
  `idAbonnement` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `affectationabonnement`
--

CREATE TABLE `affectationabonnement` (
  `idaffectationabonnement` int(11) NOT NULL,
  `idAbonne` int(11) NOT NULL,
  `idAbonnement` int(11) NOT NULL,
  `dateAffectationabonnementcol` date NOT NULL,
  `dateActivation` date NOT NULL,
  `dateDesactivation` date NOT NULL,
  `etatSync` tinyint(4) NOT NULL,
  `etatAffectation` varchar(100) DEFAULT NULL,
  `occurenceAbonnement` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `evenement`
--

CREATE TABLE `evenement` (
  `idEvenement` bigint(20) NOT NULL,
  `dateEvent` datetime NOT NULL,
  `descriptionEvent` varchar(1000) NOT NULL,
  `typeEvent` varchar(100) NOT NULL,
  `idCaisse` int(11) DEFAULT NULL,
  `idBorne` int(11) DEFAULT NULL,
  `logCaissier` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `sessions`
--

CREATE TABLE `sessions` (
  `idSessions` int(11) NOT NULL,
  `idCaisse` int(11) DEFAULT NULL,
  `logCaissier` varchar(100) DEFAULT NULL,
  `dateDebut` datetime DEFAULT NULL,
  `DateFin` datetime DEFAULT NULL,
  `montant` decimal(13,3) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `ticket`
--

CREATE TABLE `ticket` (
  `idTicket` bigint(15) NOT NULL,
  `dateHeureDebutStationnement` datetime NOT NULL,
  `dateHeureFinStationnement` datetime DEFAULT NULL,
  `etatTicket` varchar(100) NOT NULL,
  `idTarifTicket` int(11) DEFAULT NULL,
  `Tarif` decimal(13,3) DEFAULT NULL,
  `idBorneEntree` int(11) NOT NULL,
  `idBorneSortie` int(11) DEFAULT NULL,
  `LogCaissier` varchar(100) DEFAULT NULL,
  `avecTarif2` tinyint(4) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `utilisateur`
--

CREATE TABLE `utilisateur` (
  `idUtilisateur` int(11) NOT NULL,
  `nom` varchar(100) NOT NULL,
  `prenom` varchar(100) NOT NULL,
  `login` varchar(100) NOT NULL,
  `motDePasse` varchar(1000) NOT NULL,
  `Sync` tinyint(4) NOT NULL,
  `NumAccessCard` varchar(100) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `abonne`
--
ALTER TABLE `abonne`
  ADD PRIMARY KEY (`idAbonne`);

--
-- Indexes for table `abonnement`
--
ALTER TABLE `abonnement`
  ADD PRIMARY KEY (`idAbonnement`);

--
-- Indexes for table `abonnement_parking`
--
ALTER TABLE `abonnement_parking`
  ADD PRIMARY KEY (`idParking`,`idAbonnement`),
  ADD KEY `FK_AbonnementParkingIdAbonnement_idx` (`idAbonnement`);

--
-- Indexes for table `affectationabonnement`
--
ALTER TABLE `affectationabonnement`
  ADD PRIMARY KEY (`idaffectationabonnement`),
  ADD KEY `FK_idAbonnement` (`idAbonnement`),
  ADD KEY `FK_evenement_idAbonne` (`idAbonne`);

--
-- Indexes for table `evenement`
--
ALTER TABLE `evenement`
  ADD PRIMARY KEY (`idEvenement`);

--
-- Indexes for table `sessions`
--
ALTER TABLE `sessions`
  ADD PRIMARY KEY (`idSessions`);

--
-- Indexes for table `ticket`
--
ALTER TABLE `ticket`
  ADD PRIMARY KEY (`idTicket`);

--
-- Indexes for table `utilisateur`
--
ALTER TABLE `utilisateur`
  ADD PRIMARY KEY (`idUtilisateur`);

--
-- Constraints for dumped tables
--

--
-- Constraints for table `affectationabonnement`
--
ALTER TABLE `affectationabonnement`
  ADD CONSTRAINT `FK_evenement_idAbonne` FOREIGN KEY (`idAbonne`) REFERENCES `abonne` (`idAbonne`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_idAbonne` FOREIGN KEY (`idAbonne`) REFERENCES `abonne` (`idAbonne`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `FK_idAbonnement` FOREIGN KEY (`idAbonnement`) REFERENCES `abonnement` (`idAbonnement`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
