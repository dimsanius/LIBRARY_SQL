-- phpMyAdmin SQL Dump
-- version 4.5.0.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: May 23, 2016 at 05:01 PM
-- Server version: 10.0.17-MariaDB
-- PHP Version: 5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `knygynas`
--

-- --------------------------------------------------------

--
-- Table structure for table `grafikas`
--

CREATE TABLE `grafikas` (
  `ID` int(11) NOT NULL,
  `PAIMTOS_KNYGOS_AUTORIUS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PAIMTOS_KNYGOS_PAVADINIMAS` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UDK_NUMERIS_ID` int(11) NOT NULL,
  `KAS_PAEME_ID` int(11) NOT NULL,
  `KADA_PAEME` datetime DEFAULT NULL,
  `KADA_ATIDAVE` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Dumping data for table `grafikas`
--

INSERT INTO `grafikas` (`ID`, `PAIMTOS_KNYGOS_AUTORIUS`, `PAIMTOS_KNYGOS_PAVADINIMAS`, `UDK_NUMERIS_ID`, `KAS_PAEME_ID`, `KADA_PAEME`, `KADA_ATIDAVE`) VALUES
(1, 'Gogol', 'Vojna', 1, 1, NULL, NULL),
(3, 'GGG', 'HHHH', 1, 1, NULL, NULL),
(4, 'nnnn', 'mmmm', 1, 1, NULL, NULL),
(5, 'jj', 'kkk', 1, 1, NULL, NULL),
(9, 'cvcvcv', 'cvcvcv', 2, 1, NULL, NULL),
(10, 'bbb', 'nnnn', 1, 1, NULL, NULL),
(13, '??', '????', 2, 1, NULL, NULL),
(14, '????', '????', 1, 1, NULL, NULL),
(15, '?????', '????', 1, 1, NULL, NULL),
(16, '???', '????????', 2, 1, NULL, NULL),
(17, '?????', '??????', 1, 1, NULL, NULL),
(18, '??', '???', 2, 1, NULL, NULL),
(19, '???', '???', 2, 1, NULL, NULL),
(20, '????????', '??????', 1, 1, NULL, NULL),
(21, '???', '???', 1, 1, NULL, NULL),
(22, '????????????', '???', 2, 1, NULL, NULL),
(23, 'gogol', 'vasia', 2, 1, NULL, NULL),
(24, 'dddasdas', 'asdasd', 1, 1, NULL, NULL),
(25, 'ASDA', 'DASDASAAA', 1, 2, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `mokiniai`
--

CREATE TABLE `mokiniai` (
  `ID` int(11) NOT NULL,
  `VARDAS` varchar(20) NOT NULL,
  `PAVARDE` varchar(20) NOT NULL,
  `KLASE_NR` int(11) DEFAULT NULL,
  `KLASE_RAIDE` varchar(1) DEFAULT NULL,
  `KIEK_YRA_PAIMTU_KNYGU` int(11) DEFAULT '0',
  `KIEK_ATIDAVE_KNYGU` int(11) DEFAULT '0',
  `KADA_UZREGISTRUOTAS` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mokiniai`
--

INSERT INTO `mokiniai` (`ID`, `VARDAS`, `PAVARDE`, `KLASE_NR`, `KLASE_RAIDE`, `KIEK_YRA_PAIMTU_KNYGU`, `KIEK_ATIDAVE_KNYGU`, `KADA_UZREGISTRUOTAS`) VALUES
(1, 'Dmitrij', 'Santarovic', 12, 'B', 18, 0, '2016-04-03 19:16:50'),
(2, 'Kolia', 'Voron', 3, 'D', 1, 0, '2016-04-12 13:16:47'),
(3, 'vasia', 'pupkin', 2, 'A', 0, 0, '2016-04-12 13:19:49');

-- --------------------------------------------------------

--
-- Table structure for table `udk`
--

CREATE TABLE `udk` (
  `ID` int(11) NOT NULL,
  `UDK_APRASYMAS` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `udk`
--

INSERT INTO `udk` (`ID`, `UDK_APRASYMAS`) VALUES
(1, 'FANTASTIKA'),
(2, 'triller');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `grafikas`
--
ALTER TABLE `grafikas`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `UDK_NUMERIS_ID` (`UDK_NUMERIS_ID`),
  ADD KEY `KAS_PAEME_ID` (`KAS_PAEME_ID`);

--
-- Indexes for table `mokiniai`
--
ALTER TABLE `mokiniai`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `udk`
--
ALTER TABLE `udk`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `grafikas`
--
ALTER TABLE `grafikas`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;
--
-- AUTO_INCREMENT for table `mokiniai`
--
ALTER TABLE `mokiniai`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;
--
-- AUTO_INCREMENT for table `udk`
--
ALTER TABLE `udk`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `grafikas`
--
ALTER TABLE `grafikas`
  ADD CONSTRAINT `grafikas_ibfk_1` FOREIGN KEY (`UDK_NUMERIS_ID`) REFERENCES `udk` (`ID`),
  ADD CONSTRAINT `grafikas_ibfk_2` FOREIGN KEY (`KAS_PAEME_ID`) REFERENCES `mokiniai` (`ID`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
