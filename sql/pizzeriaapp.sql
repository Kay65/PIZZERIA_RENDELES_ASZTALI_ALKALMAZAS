-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2024. Ápr 22. 08:24
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `pizzeriaapp`
--
CREATE DATABASE IF NOT EXISTS `pizzeriaapp` DEFAULT CHARACTER SET utf8 COLLATE utf8_hungarian_ci;
USE `pizzeriaapp`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `felhasználó`
--

CREATE TABLE `felhasználó` (
  `ID` int(11) NOT NULL,
  `Email` text NOT NULL,
  `Jelszó` text NOT NULL,
  `Admin` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `felhasználó`
--

INSERT INTO `felhasználó` (`ID`, `Email`, `Jelszó`, `Admin`) VALUES
(1, 'Admin@Admin.hu', 'Admin123', 1);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `kosár`
--

CREATE TABLE `kosár` (
  `ID` int(11) NOT NULL,
  `PizzaID` int(11) NOT NULL,
  `Mennyiség` int(11) NOT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `pizza`
--

CREATE TABLE `pizza` (
  `ID` int(11) NOT NULL,
  `Name` text NOT NULL,
  `Price` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- A tábla adatainak kiíratása `pizza`
--

INSERT INTO `pizza` (`ID`, `Name`, `Price`) VALUES
(1, 'Sajtos Pizza', 2510),
(2, 'Hawaii Pizza', 3050),
(3, 'Szalámis Pizza', 2770),
(4, 'Csípős Szalámis Pizza', 2870),
(5, 'Gombás Pizza', 2990),
(6, 'Sonkás Pizza', 3000);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendeleselem`
--

CREATE TABLE `rendeleselem` (
  `ID` int(11) NOT NULL,
  `RendelésID` int(11) NOT NULL,
  `PizzaID` int(11) NOT NULL,
  `Menyiség` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `rendelés`
--

CREATE TABLE `rendelés` (
  `ID` int(11) NOT NULL,
  `Állapot` text NOT NULL,
  `Várható kiszállitás` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  `Osszár` int(11) NOT NULL,
  `kód` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `felhasználó`
--
ALTER TABLE `felhasználó`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `Jelszó` (`Jelszó`) USING HASH,
  ADD UNIQUE KEY `Email_2` (`Email`) USING HASH;

--
-- A tábla indexei `kosár`
--
ALTER TABLE `kosár`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `pizza`
--
ALTER TABLE `pizza`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `rendeleselem`
--
ALTER TABLE `rendeleselem`
  ADD PRIMARY KEY (`ID`);

--
-- A tábla indexei `rendelés`
--
ALTER TABLE `rendelés`
  ADD PRIMARY KEY (`ID`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `felhasználó`
--
ALTER TABLE `felhasználó`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT a táblához `kosár`
--
ALTER TABLE `kosár`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT a táblához `pizza`
--
ALTER TABLE `pizza`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `rendeleselem`
--
ALTER TABLE `rendeleselem`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT a táblához `rendelés`
--
ALTER TABLE `rendelés`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
