-- ============================================================
-- PR.OpenGym - Script de creación de BD local para pruebas
-- Generado desde la BD de producción (MYSQL5044.site4now.net)
-- Fecha: 2026-09-24
-- ============================================================

-- Crear la base de datos local
CREATE DATABASE IF NOT EXISTS `opengym_local` 
  DEFAULT CHARACTER SET utf8mb4 
  COLLATE utf8mb4_0900_ai_ci;

USE `opengym_local`;

-- ============================================================
-- 1. TABLAS DE EF CORE MIGRATIONS
-- ============================================================

CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ============================================================
-- 2. TABLAS DE ASP.NET IDENTITY
-- ============================================================

CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `UserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedUserName` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `Email` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `NormalizedEmail` varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `SecurityStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ClaimType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderKey` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RoleId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `LoginProvider` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Name` varchar(128) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Value` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ============================================================
-- 3. TABLAS DE NEGOCIO
-- ============================================================

-- Products (incluye Memberships via TPH - columna Discriminator)
CREATE TABLE IF NOT EXISTS `products` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` decimal(18,4) NOT NULL,
  `Description` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Discriminator` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Period` int DEFAULT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Branches (sucursales)
CREATE TABLE IF NOT EXISTS `branches` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- AssociateMemberships
CREATE TABLE IF NOT EXISTS `associatememberships` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `MembershipId` int DEFAULT NULL,
  `From` datetime(6) DEFAULT NULL,
  `To` datetime(6) DEFAULT NULL,
  `MembershipStatus` int NOT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AssociateMemberships_MembershipId` (`MembershipId`),
  CONSTRAINT `FK_AssociateMemberships_Products_MembershipId` FOREIGN KEY (`MembershipId`) REFERENCES `products` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Associates (socios)
CREATE TABLE IF NOT EXISTS `associates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `LastName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Gender` int DEFAULT NULL,
  `DateBirth` datetime(6) DEFAULT NULL,
  `Age` int DEFAULT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Phone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Facebook` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ImgPath` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `BranchId` int DEFAULT NULL,
  `AssociateMembershipId` int DEFAULT NULL,
  `Status` int NOT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Associates_AssociateMembershipId` (`AssociateMembershipId`),
  KEY `IX_Associates_BranchId` (`BranchId`),
  CONSTRAINT `FK_Associates_AssociateMemberships_AssociateMembershipId` FOREIGN KEY (`AssociateMembershipId`) REFERENCES `associatememberships` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Associates_Branches_BranchId` FOREIGN KEY (`BranchId`) REFERENCES `branches` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- AssociateDetails (dirección del socio)
CREATE TABLE IF NOT EXISTS `associatedetails` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AssociateId` int NOT NULL,
  `Street` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Number` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Neighborhood` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ZipCode` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `City` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `State` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Country` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AssociateDetails_AssociateId` (`AssociateId`),
  CONSTRAINT `FK_AssociateDetails_Associates_AssociateId` FOREIGN KEY (`AssociateId`) REFERENCES `associates` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Payments (pagos)
CREATE TABLE IF NOT EXISTS `payments` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AssociateId` int DEFAULT NULL,
  `Amount` decimal(18,4) NOT NULL,
  `Concept` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `ProductId` int DEFAULT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Payments_AssociateId` (`AssociateId`),
  KEY `IX_Payments_ProductId` (`ProductId`),
  CONSTRAINT `FK_Payments_Associates_AssociateId` FOREIGN KEY (`AssociateId`) REFERENCES `associates` (`Id`) ON DELETE SET NULL ON UPDATE SET NULL,
  CONSTRAINT `FK_Payments_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=100 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- CheckIns (registros de entrada)
CREATE TABLE IF NOT EXISTS `checkins` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AssociateId` int NOT NULL,
  `CreatedOn` datetime(6) NOT NULL,
  `ModifiedOn` datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_CheckIns_AssociateId` (`AssociateId`),
  CONSTRAINT `FK_CheckIns_Associates_AssociateId` FOREIGN KEY (`AssociateId`) REFERENCES `associates` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- ============================================================
-- 4. DATOS SEMILLA (SEED DATA)
-- ============================================================

-- Migraciones de EF Core
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20231015012824_AssociateMembershipAssociateMembershipId', '7.0.9'),
('20231019050020_AssociateSeedData', '7.0.9');

-- Roles
INSERT INTO `aspnetroles` (`Id`, `Name`, `NormalizedName`, `ConcurrencyStamp`) VALUES
('257020d9-bc80-4821-a6ca-ffef7c787dfa', 'User', 'USER', '837e6317-0af0-429b-8de6-cdf9f8bbe90d'),
('28fc5ae8-c637-4bfd-8aa4-23c785a28bfb', 'Administrator', 'ADMINISTRATOR', 'cabdf966-1251-4778-8af0-3c7bfa0406a8');

-- Usuarios del sistema (password: misma que producción)
INSERT INTO `aspnetusers` (`Id`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
('20e6c427-d499-4fb3-b53b-0e44d99767b7', 'user@316fitness.com', 'USER@316FITNESS.COM', NULL, NULL, 1, 'AQAAAAEAACcQAAAAEAiani0i3CtThr1lPh2pWWd6EJAlAtid2MQtGYoLg5i0OlfVNxP5HpxEZJOBZ8Ftqg==', 'cca4e424-d805-4a7f-904c-b448dc5399ad', '092eb93c-78db-44d7-a6d7-8923a7375b00', NULL, 0, 0, NULL, 0, 0),
('57c7963f-c1fb-48b9-847a-38deadd11453', 'admin@316fitness.com', 'ADMIN@316FITNESS.COM', NULL, NULL, 1, 'AQAAAAEAACcQAAAAEAiani0i3CtThr1lPh2pWWd6EJAlAtid2MQtGYoLg5i0OlfVNxP5HpxEZJOBZ8Ftqg==', '388f0561-eaed-4189-90dd-caa303d6102e', '74573664-12a1-490b-ab9d-70f9823d61a3', NULL, 0, 0, NULL, 0, 0);

-- Asignación de roles
INSERT INTO `aspnetuserroles` (`UserId`, `RoleId`) VALUES
('20e6c427-d499-4fb3-b53b-0e44d99767b7', '257020d9-bc80-4821-a6ca-ffef7c787dfa'),
('57c7963f-c1fb-48b9-847a-38deadd11453', '28fc5ae8-c637-4bfd-8aa4-23c785a28bfb');

-- Sucursal
INSERT INTO `branches` (`Id`, `Name`, `Address`, `CreatedOn`, `ModifiedOn`) VALUES
(1, '3:16 Fitness - Río Mayo Delta', 'Blvd. la Luz, Las Cruces, 37290 León, Gto.', '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000');

-- Productos / Membresías
INSERT INTO `products` (`Id`, `Name`, `Price`, `Description`, `Discriminator`, `Period`, `CreatedOn`, `ModifiedOn`) VALUES
(1,  'MENSUAL',    370.0000, NULL, 'Membership',  31, '0001-01-01 00:00:00.000000', '2025-01-22 18:31:16.426743'),
(2,  'VISITA',      50.0000, NULL, 'Membership',   1, '0001-01-01 00:00:00.000000', '2025-01-22 18:30:32.492833'),
(6,  'SEMANAL',    200.0000, NULL, 'Membership',   7, '0001-01-01 00:00:00.000000', '2025-09-21 09:02:15.022072'),
(10, 'QUINCENAL',  250.0000, NULL, 'Membership',  15, '0001-01-01 00:00:00.000000', '2025-01-22 18:31:03.435316'),
(11, 'ANUAL',     4070.0000, NULL, 'Membership', 365, '0001-01-01 00:00:00.000000', '2025-02-16 10:03:32.897615');

-- ============================================================
-- 5. DATOS DE PRUEBA (SOCIOS DE EJEMPLO)
-- ============================================================

-- Membresías de ejemplo
INSERT INTO `associatememberships` (`Id`, `MembershipId`, `From`, `To`, `MembershipStatus`, `CreatedOn`, `ModifiedOn`) VALUES
(1, 1, '2026-09-01 10:00:00.000000', '2026-10-01 10:00:00.000000', 1, '2026-09-01 10:00:00.000000', '2026-09-01 10:00:00.000000'),
(2, 1, '2026-09-15 08:30:00.000000', '2026-10-15 08:30:00.000000', 1, '2026-09-15 08:30:00.000000', '2026-09-15 08:30:00.000000'),
(3, 6, '2026-09-20 09:00:00.000000', '2026-09-27 09:00:00.000000', 1, '2026-09-20 09:00:00.000000', '2026-09-20 09:00:00.000000'),
(4, 11, '2026-01-10 12:00:00.000000', '2027-01-10 12:00:00.000000', 1, '2026-01-10 12:00:00.000000', '2026-01-10 12:00:00.000000'),
(5, 1, '2026-08-01 10:00:00.000000', '2026-09-01 10:00:00.000000', 2, '2026-08-01 10:00:00.000000', '2026-08-01 10:00:00.000000');

-- Socios de ejemplo
INSERT INTO `associates` (`Id`, `FirstName`, `LastName`, `Gender`, `DateBirth`, `Age`, `Email`, `Phone`, `Facebook`, `ImgPath`, `BranchId`, `AssociateMembershipId`, `Status`, `CreatedOn`, `ModifiedOn`) VALUES
(1, 'Juan',    'Pérez García',     0, '1990-05-15 00:00:00.000000', 36, 'juan.perez@email.com',    '4771234567', NULL, NULL, 1, 1, 1, '2026-09-01 10:00:00.000000', '2026-09-01 10:00:00.000000'),
(2, 'María',   'López Hernández',  1, '1995-08-22 00:00:00.000000', 31, 'maria.lopez@email.com',   '4779876543', NULL, NULL, 1, 2, 1, '2026-09-15 08:30:00.000000', '2026-09-15 08:30:00.000000'),
(3, 'Carlos',  'Ramírez Torres',   0, '1988-12-03 00:00:00.000000', 37, 'carlos.ramirez@email.com','4775551234', NULL, NULL, 1, 3, 1, '2026-09-20 09:00:00.000000', '2026-09-20 09:00:00.000000'),
(4, 'Ana',     'Martínez Ruiz',    1, '2000-03-10 00:00:00.000000', 26, 'ana.martinez@email.com',  '4778889999', NULL, NULL, 1, 4, 1, '2026-01-10 12:00:00.000000', '2026-01-10 12:00:00.000000'),
(5, 'Roberto', 'Sánchez Morales',  0, '1985-07-28 00:00:00.000000', 41, 'roberto.sanchez@email.com','4776667777', NULL, NULL, 1, 5, 0, '2026-08-01 10:00:00.000000', '2026-08-01 10:00:00.000000');

-- Detalles de dirección
INSERT INTO `associatedetails` (`Id`, `AssociateId`, `Street`, `Number`, `Neighborhood`, `ZipCode`, `City`, `State`, `Country`, `CreatedOn`, `ModifiedOn`) VALUES
(1, 1, 'Av. López Mateos',  '1234',  'Centro',          '37000', 'León', 'Guanajuato', 'México', '2026-09-01 10:00:00.000000', '2026-09-01 10:00:00.000000'),
(2, 2, 'Blvd. Adolfo López','567',   'Las Hilamas',     '37200', 'León', 'Guanajuato', 'México', '2026-09-15 08:30:00.000000', '2026-09-15 08:30:00.000000'),
(3, 3, 'Calle Madero',      '89',    'San Juan de Dios','37100', 'León', 'Guanajuato', 'México', '2026-09-20 09:00:00.000000', '2026-09-20 09:00:00.000000'),
(4, 4, 'Paseo del Moral',   '2345',  'Jardines del Moral','37160','León','Guanajuato', 'México', '2026-01-10 12:00:00.000000', '2026-01-10 12:00:00.000000'),
(5, 5, 'Blvd. Torres Landa','678',   'San Isidro',      '37500', 'León', 'Guanajuato', 'México', '2026-08-01 10:00:00.000000', '2026-08-01 10:00:00.000000');

-- Pagos de ejemplo
INSERT INTO `payments` (`Id`, `AssociateId`, `Amount`, `Concept`, `ProductId`, `CreatedOn`, `ModifiedOn`) VALUES
(1, 1, 370.0000, 'Pago Membresia', 1, '2026-09-01 10:00:00.000000', '2026-09-01 10:00:00.000000'),
(2, 2, 370.0000, 'Pago Membresia', 1, '2026-09-15 08:30:00.000000', '2026-09-15 08:30:00.000000'),
(3, 3, 200.0000, 'Pago Membresia', 6, '2026-09-20 09:00:00.000000', '2026-09-20 09:00:00.000000'),
(4, 4, 4070.0000,'Pago Membresia', 11,'2026-01-10 12:00:00.000000', '2026-01-10 12:00:00.000000'),
(5, 5, 370.0000, 'Pago Membresia', 1, '2026-08-01 10:00:00.000000', '2026-08-01 10:00:00.000000');

-- ============================================================
-- 6. VERIFICACIÓN
-- ============================================================

SELECT 'Tablas creadas:' AS Info;
SELECT TABLE_NAME, TABLE_ROWS 
FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'opengym_local' 
ORDER BY TABLE_NAME;

-- ============================================================
-- INSTRUCCIONES DE USO:
-- ============================================================
-- 
-- 1. Asegúrate de tener MySQL Server corriendo localmente
--
-- 2. Ejecuta este script:
--    mysql -u root -p < scripts/create_local_db.sql
--
-- 3. Actualiza los connection strings en ambos proyectos:
--
--    PR.OpenGym.API/appsettings.json:
--    "DefaultConnectionMysql": "Server=localhost;Database=opengym_local;Uid=root;Pwd=TU_PASSWORD"
--
--    PR.OpenGym.Web/appsettings.json:
--    "DefaultConnectionMysql": "Server=localhost;Database=opengym_local;Uid=root;Pwd=TU_PASSWORD"
--
-- 4. Usuarios del sistema para login:
--    - Admin: admin@316fitness.com  (rol: Administrator)
--    - User:  user@316fitness.com   (rol: User)
--    - (la contraseña es la misma que tenían en producción)
--
-- 5. Membresías disponibles:
--    - VISITA    ($50,   1 día)
--    - SEMANAL   ($200,  7 días)
--    - QUINCENAL ($250, 15 días)
--    - MENSUAL   ($370, 31 días)
--    - ANUAL     ($4070, 365 días)
--
