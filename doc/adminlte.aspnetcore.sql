/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 80016
Source Host           : localhost:3306
Source Database       : adminlte.aspnetcore

Target Server Type    : MYSQL
Target Server Version : 80016
File Encoding         : 65001

Date: 2019-12-21 22:03:00
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for departments
-- ----------------------------
DROP TABLE IF EXISTS `departments`;
CREATE TABLE `departments` (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Manager` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ContactNumber` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `IsDeleted` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of departments
-- ----------------------------
INSERT INTO `departments` VALUES ('08d785e5-bff3-88fc-cdf0-32176b958a00', '河北公司', 'hb', '唐彦磊', null, null, '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', null, '0');
INSERT INTO `departments` VALUES ('08d785e5-c88e-2d85-38fa-48b3a3133336', '邯郸事业部', 'hd', null, null, null, '08d785e5-bff3-88fc-cdf0-32176b958a00', '00000000-0000-0000-0000-000000000000', null, '0');
INSERT INTO `departments` VALUES ('b85885ea-2eb1-4933-826e-1bd00e95efad', '系统', null, null, null, null, '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', null, '0');

-- ----------------------------
-- Table structure for menus
-- ----------------------------
DROP TABLE IF EXISTS `menus`;
CREATE TABLE `menus` (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ParentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `SerialNumber` int(11) NOT NULL DEFAULT '9999',
  `Text` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Code` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Url` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Type` int(11) NOT NULL,
  `Icon` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of menus
-- ----------------------------
INSERT INTO `menus` VALUES ('08d785e4-ae02-8982-e515-8098d557da1d', '00000000-0000-0000-0000-000000000000', '0', '组织机构管理', 'Department', '/Department/Index', '0', 'fa fa-link', null);
INSERT INTO `menus` VALUES ('08d785e4-ae05-ad7f-da89-d96921389ab4', '00000000-0000-0000-0000-000000000000', '1', '角色管理', 'Role', '/Role/Index', '0', 'fa fa-link', null);
INSERT INTO `menus` VALUES ('08d785e4-ae05-b390-b4c5-a8fda09477e7', '00000000-0000-0000-0000-000000000000', '2', '用户管理', 'User', '/User/Index', '0', 'fa fa-link', null);
INSERT INTO `menus` VALUES ('08d785e4-ae05-b56b-8911-13e12d8cbfd4', '00000000-0000-0000-0000-000000000000', '3', '功能管理', 'Menu', '/Menu/Index', '0', 'fa fa-link', null);

-- ----------------------------
-- Table structure for rolemenus
-- ----------------------------
DROP TABLE IF EXISTS `rolemenus`;
CREATE TABLE `rolemenus` (
  `RoleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `MenuId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`MenuId`,`RoleId`),
  KEY `FK_RoleMenus_Roles_RoleId` (`RoleId`) USING BTREE,
  CONSTRAINT `rolemenus_ibfk_1` FOREIGN KEY (`MenuId`) REFERENCES `menus` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `rolemenus_ibfk_2` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of rolemenus
-- ----------------------------
INSERT INTO `rolemenus` VALUES ('08d785e5-5600-47a7-4707-298cfdc27f4d', '08d785e4-ae05-ad7f-da89-d96921389ab4');
INSERT INTO `rolemenus` VALUES ('08d785e5-5600-47a7-4707-298cfdc27f4d', '08d785e4-ae05-b390-b4c5-a8fda09477e7');
INSERT INTO `rolemenus` VALUES ('08d785e5-5600-47a7-4707-298cfdc27f4d', '08d785e4-ae05-b56b-8911-13e12d8cbfd4');

-- ----------------------------
-- Table structure for roles
-- ----------------------------
DROP TABLE IF EXISTS `roles`;
CREATE TABLE `roles` (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Code` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Name` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `Remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of roles
-- ----------------------------
INSERT INTO `roles` VALUES ('08d785e5-5600-47a7-4707-298cfdc27f4d', 'hb000', '河北公司管理员', '00000000-0000-0000-0000-000000000000', '2019-12-21 15:13:53', null);

-- ----------------------------
-- Table structure for userroles
-- ----------------------------
DROP TABLE IF EXISTS `userroles`;
CREATE TABLE `userroles` (
  `UserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `RoleId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `FK_UserRoles_Roles_RoleId` (`RoleId`) USING BTREE,
  CONSTRAINT `userroles_ibfk_1` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `userroles_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of userroles
-- ----------------------------
INSERT INTO `userroles` VALUES ('3fe995e3-397d-492b-b836-e8c1c177d6e0', '08d785e5-5600-47a7-4707-298cfdc27f4d');

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `Id` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `UserName` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Name` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `EMail` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `MobileNumber` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `Remarks` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL,
  `CreateUserId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `LastLoginTime` datetime NOT NULL,
  `LoginTimes` int(11) NOT NULL,
  `DepartmentId` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `IsDeleted` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Users_Departments_DepartmentId` (`DepartmentId`) USING BTREE,
  CONSTRAINT `users_ibfk_1` FOREIGN KEY (`DepartmentId`) REFERENCES `departments` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('08d785e4-a0f8-f513-df36-2347e6ff1dfa', 'admin', '123456', '超级管理员', null, null, null, '00000000-0000-0000-0000-000000000000', null, '0001-01-01 00:00:00', '0', 'b85885ea-2eb1-4933-826e-1bd00e95efad', '0');
INSERT INTO `users` VALUES ('3fe995e3-397d-492b-b836-e8c1c177d6e0', 'gaofen13', '123', '唐彦磊', null, null, null, '00000000-0000-0000-0000-000000000000', null, '0001-01-01 00:00:00', '0', '08d785e5-bff3-88fc-cdf0-32176b958a00', '0');
