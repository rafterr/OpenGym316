-- ============================================================
-- PR.OpenGym - Recibos de pago
-- 1. Metodo de pago en payments (0 = Efectivo, 1 = Transferencia)
-- 2. Tabla receipts: copia congelada de los datos del cobro.
--    El folio se muestra como R-{Id:000000} (consecutivo simple).
-- Ejecutar una sola vez por base de datos.
-- ============================================================

ALTER TABLE `payments`
  ADD COLUMN `PaymentMethod` int NOT NULL DEFAULT 0 AFTER `ProductId`;

CREATE TABLE IF NOT EXISTS `receipts` (
  `Id`             int NOT NULL AUTO_INCREMENT,
  `PaymentId`      int NOT NULL,
  `AssociateId`    int DEFAULT NULL,
  `AssociateName`  varchar(200) NOT NULL,
  `MembershipName` varchar(100) NOT NULL,
  `PeriodFrom`     datetime(6) NOT NULL,
  `PeriodTo`       datetime(6) NOT NULL,
  `Amount`         decimal(18,4) NOT NULL,
  `PaymentMethod`  int NOT NULL DEFAULT 0,
  `BranchName`     varchar(200) DEFAULT NULL,
  `BranchAddress`  varchar(300) DEFAULT NULL,
  `IssuedBy`       varchar(256) DEFAULT NULL,
  `Status`         int NOT NULL DEFAULT 0,   -- 0 Vigente, 1 Cancelado
  `CancelReason`   varchar(300) DEFAULT NULL,
  `CreatedOn`      datetime(6) NOT NULL,
  `ModifiedOn`     datetime(6) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Receipts_PaymentId` (`PaymentId`),
  KEY `IX_Receipts_AssociateId` (`AssociateId`),
  KEY `IX_Receipts_CreatedOn` (`CreatedOn`),
  CONSTRAINT `FK_Receipts_Payments_PaymentId` FOREIGN KEY (`PaymentId`) REFERENCES `payments` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Receipts_Associates_AssociateId` FOREIGN KEY (`AssociateId`) REFERENCES `associates` (`Id`) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
