-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: 2016-10-09 05:39:29
-- 服务器版本： 10.1.8-MariaDB
-- PHP Version: 5.6.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `oauth`
--

-- --------------------------------------------------------

--
-- 表的结构 `migrations`
--

CREATE TABLE `migrations` (
  `migration` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `batch` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `migrations`
--

INSERT INTO `migrations` (`migration`, `batch`) VALUES
('2014_04_24_110151_create_oauth_scopes_table', 1),
('2014_04_24_110304_create_oauth_grants_table', 1),
('2014_04_24_110403_create_oauth_grant_scopes_table', 1),
('2014_04_24_110459_create_oauth_clients_table', 1),
('2014_04_24_110557_create_oauth_client_endpoints_table', 1),
('2014_04_24_110705_create_oauth_client_scopes_table', 1),
('2014_04_24_110817_create_oauth_client_grants_table', 1),
('2014_04_24_111002_create_oauth_sessions_table', 1),
('2014_04_24_111109_create_oauth_session_scopes_table', 1),
('2014_04_24_111254_create_oauth_auth_codes_table', 1),
('2014_04_24_111403_create_oauth_auth_code_scopes_table', 1),
('2014_04_24_111518_create_oauth_access_tokens_table', 1),
('2014_04_24_111657_create_oauth_access_token_scopes_table', 1),
('2014_04_24_111810_create_oauth_refresh_tokens_table', 1),
('2014_10_12_000000_create_users_table', 1),
('2014_10_12_100000_create_password_resets_table', 1);

-- --------------------------------------------------------

--
-- 表的结构 `oauth_access_tokens`
--

CREATE TABLE `oauth_access_tokens` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `session_id` int(10) UNSIGNED NOT NULL,
  `expire_time` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_access_tokens`
--

INSERT INTO `oauth_access_tokens` (`id`, `session_id`, `expire_time`, `created_at`, `updated_at`) VALUES
('1HOMucn021gdvP4pwYzLUK9QvnsI9e0U8dNfaU6Z', 3, 1475920555, '2016-10-08 00:55:55', '2016-10-08 00:55:55'),
('2RhXZdtnrVvCdaSCeHgw3oAaAcrTNmuquUWbPTGC', 9, 1475925766, '2016-10-08 02:22:46', '2016-10-08 02:22:46'),
('3jLFuqdkVCLwv2jvfoBTbwZBYYdM2ET70hoIe8i0', 18, 1475981735, '2016-10-08 17:55:35', '2016-10-08 17:55:35'),
('6ZZVTKNt5R9vkpK0l4xW4x2SWULF1P4yuSSYL6fh', 8, 1475921682, '2016-10-08 01:14:42', '2016-10-08 01:14:42'),
('aCM3IQ7KS0CnGOsgB4h8B07ts6dYI8W53OEZSLtt', 2, 1475920469, '2016-10-08 00:54:29', '2016-10-08 00:54:29'),
('cckFRk4WqKVW5DPGepEsC0e8NwvHKIXiV6sU2gri', 14, 1475978113, '2016-10-08 16:55:13', '2016-10-08 16:55:13'),
('eGc1ADLV1NCdFLjXbr8q2NJ3878Ai2KpMJFvGmIz', 19, 1475981934, '2016-10-08 17:58:54', '2016-10-08 17:58:54'),
('jZcTHf1nFKWvBCcjHjQwfPdzcpx2zegDBjm1fmNk', 20, 1475982401, '2016-10-08 18:06:41', '2016-10-08 18:06:41'),
('K1fr8H1pEpHBVfyaysR58xZMngBpRmBoIpLbJmoR', 6, 1475921454, '2016-10-08 01:10:54', '2016-10-08 01:10:54'),
('pF5l94P7dk9edQjXTMuYt0iEQDmr55O9cczdAHDm', 15, 1475978276, '2016-10-08 16:57:56', '2016-10-08 16:57:56'),
('y9h91Rqh4fuPVfmJ8VBJCzuHvvlpqBPJT8nxYIH2', 17, 1475981596, '2016-10-08 17:53:16', '2016-10-08 17:53:16'),
('ydh4pf41pzpzykH9SaLGaIxHnmDLxbPHjbhckcvE', 5, 1475921029, '2016-10-08 01:03:49', '2016-10-08 01:03:49');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_access_token_scopes`
--

CREATE TABLE `oauth_access_token_scopes` (
  `id` int(10) UNSIGNED NOT NULL,
  `access_token_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `scope_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_access_token_scopes`
--

INSERT INTO `oauth_access_token_scopes` (`id`, `access_token_id`, `scope_id`, `created_at`, `updated_at`) VALUES
(1, 'aCM3IQ7KS0CnGOsgB4h8B07ts6dYI8W53OEZSLtt', 'scope1', '2016-10-08 00:54:29', '2016-10-08 00:54:29'),
(2, '1HOMucn021gdvP4pwYzLUK9QvnsI9e0U8dNfaU6Z', 'scope1', '2016-10-08 00:55:55', '2016-10-08 00:55:55'),
(3, 'ydh4pf41pzpzykH9SaLGaIxHnmDLxbPHjbhckcvE', 'scope1', '2016-10-08 01:03:49', '2016-10-08 01:03:49'),
(4, 'K1fr8H1pEpHBVfyaysR58xZMngBpRmBoIpLbJmoR', 'scope1', '2016-10-08 01:10:54', '2016-10-08 01:10:54'),
(5, '6ZZVTKNt5R9vkpK0l4xW4x2SWULF1P4yuSSYL6fh', 'scope1', '2016-10-08 01:14:42', '2016-10-08 01:14:42'),
(6, '2RhXZdtnrVvCdaSCeHgw3oAaAcrTNmuquUWbPTGC', 'scope1', '2016-10-08 02:22:46', '2016-10-08 02:22:46'),
(7, 'cckFRk4WqKVW5DPGepEsC0e8NwvHKIXiV6sU2gri', 'scope1', '2016-10-08 16:55:13', '2016-10-08 16:55:13'),
(8, 'pF5l94P7dk9edQjXTMuYt0iEQDmr55O9cczdAHDm', 'scope1', '2016-10-08 16:57:56', '2016-10-08 16:57:56'),
(9, 'y9h91Rqh4fuPVfmJ8VBJCzuHvvlpqBPJT8nxYIH2', 'scope1', '2016-10-08 17:53:16', '2016-10-08 17:53:16'),
(10, '3jLFuqdkVCLwv2jvfoBTbwZBYYdM2ET70hoIe8i0', 'scope1', '2016-10-08 17:55:35', '2016-10-08 17:55:35'),
(11, 'eGc1ADLV1NCdFLjXbr8q2NJ3878Ai2KpMJFvGmIz', 'scope1', '2016-10-08 17:58:54', '2016-10-08 17:58:54'),
(12, 'jZcTHf1nFKWvBCcjHjQwfPdzcpx2zegDBjm1fmNk', 'scope1', '2016-10-08 18:06:41', '2016-10-08 18:06:41');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_auth_codes`
--

CREATE TABLE `oauth_auth_codes` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `session_id` int(10) UNSIGNED NOT NULL,
  `redirect_uri` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `expire_time` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_auth_codes`
--

INSERT INTO `oauth_auth_codes` (`id`, `session_id`, `redirect_uri`, `expire_time`, `created_at`, `updated_at`) VALUES
('1P3qRcnAdCi24K59VcivZ7sh19Iil8W5WxRNk2Pi', 13, 'http://121.42.170.15:9494/callback', 1475927333, '2016-10-08 02:48:53', '2016-10-08 02:48:53'),
('622t5Bi0eKhRHljyUeJQV38gNrLsvyG4Igr52E7K', 4, 'http://121.42.170.15:9494/callback', 1475920960, '2016-10-08 01:02:40', '2016-10-08 01:02:40'),
('CjCOMdH00lhDTUuNRXHzKZRRCWa1sOrmcCoEzARh', 12, 'http://121.42.170.15:9494/callback', 1475927312, '2016-10-08 02:48:32', '2016-10-08 02:48:32'),
('hddoyjTvUJYVE4IaEd12jGdRtrBDBTXpuSy3uouI', 16, 'http://121.42.170.15:9494/callback', 1475981590, '2016-10-08 17:53:10', '2016-10-08 17:53:10'),
('HnE5PLlBSWOfoSvnTyXDGrhiNFel56zheBQX7EmQ', 1, 'http://121.42.170.15:9494/callback', 1475916332, '2016-10-07 23:45:32', '2016-10-07 23:45:32'),
('JlfuW5cJYvBQtu3vyywhCsdwI7EsClWMzLbg7lsH', 7, 'http://121.42.170.15:9494/callback', 1475921575, '2016-10-08 01:12:55', '2016-10-08 01:12:55'),
('OguAHh39kfuDf0i0mgqBaPXjIdouWRsL7H6a7IA3', 11, 'http://121.42.170.15:9494/callback', 1475927304, '2016-10-08 02:48:24', '2016-10-08 02:48:24'),
('r5nCHpV2ajweGUmGfS0Wzuhat58wUQAqSCQVVvIa', 10, 'http://121.42.170.15:9494/callback', 1475927301, '2016-10-08 02:48:21', '2016-10-08 02:48:21');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_auth_code_scopes`
--

CREATE TABLE `oauth_auth_code_scopes` (
  `id` int(10) UNSIGNED NOT NULL,
  `auth_code_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `scope_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_auth_code_scopes`
--

INSERT INTO `oauth_auth_code_scopes` (`id`, `auth_code_id`, `scope_id`, `created_at`, `updated_at`) VALUES
(1, 'HnE5PLlBSWOfoSvnTyXDGrhiNFel56zheBQX7EmQ', 'scope1', '2016-10-07 23:45:32', '2016-10-07 23:45:32'),
(4, '622t5Bi0eKhRHljyUeJQV38gNrLsvyG4Igr52E7K', 'scope1', '2016-10-08 01:02:40', '2016-10-08 01:02:40'),
(7, 'JlfuW5cJYvBQtu3vyywhCsdwI7EsClWMzLbg7lsH', 'scope1', '2016-10-08 01:12:55', '2016-10-08 01:12:55'),
(10, 'r5nCHpV2ajweGUmGfS0Wzuhat58wUQAqSCQVVvIa', 'scope1', '2016-10-08 02:48:21', '2016-10-08 02:48:21'),
(11, 'OguAHh39kfuDf0i0mgqBaPXjIdouWRsL7H6a7IA3', 'scope1', '2016-10-08 02:48:24', '2016-10-08 02:48:24'),
(12, 'CjCOMdH00lhDTUuNRXHzKZRRCWa1sOrmcCoEzARh', 'scope1', '2016-10-08 02:48:32', '2016-10-08 02:48:32'),
(13, '1P3qRcnAdCi24K59VcivZ7sh19Iil8W5WxRNk2Pi', 'scope1', '2016-10-08 02:48:53', '2016-10-08 02:48:53'),
(16, 'hddoyjTvUJYVE4IaEd12jGdRtrBDBTXpuSy3uouI', 'scope1', '2016-10-08 17:53:10', '2016-10-08 17:53:10');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_clients`
--

CREATE TABLE `oauth_clients` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `secret` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `name` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_clients`
--

INSERT INTO `oauth_clients` (`id`, `secret`, `name`, `created_at`, `updated_at`) VALUES
('crm2', 'iIR22w9bZmRlk7HwnOCT7k6GfmEutdoX', 'crm2', '2016-10-08 03:22:30', '2016-10-08 03:22:33');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_client_endpoints`
--

CREATE TABLE `oauth_client_endpoints` (
  `id` int(10) UNSIGNED NOT NULL,
  `client_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `redirect_uri` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_client_endpoints`
--

INSERT INTO `oauth_client_endpoints` (`id`, `client_id`, `redirect_uri`, `created_at`, `updated_at`) VALUES
(1, 'crm2', 'http://121.42.170.15:9494/callback', '2016-10-08 03:28:59', '2016-10-08 03:29:03');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_client_grants`
--

CREATE TABLE `oauth_client_grants` (
  `id` int(10) UNSIGNED NOT NULL,
  `client_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `grant_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- 表的结构 `oauth_client_scopes`
--

CREATE TABLE `oauth_client_scopes` (
  `id` int(10) UNSIGNED NOT NULL,
  `client_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `scope_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_client_scopes`
--

INSERT INTO `oauth_client_scopes` (`id`, `client_id`, `scope_id`, `created_at`, `updated_at`) VALUES
(1, 'crm2', 'scope1', '2016-10-08 03:24:25', '2016-10-08 03:24:27');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_grants`
--

CREATE TABLE `oauth_grants` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- 表的结构 `oauth_grant_scopes`
--

CREATE TABLE `oauth_grant_scopes` (
  `id` int(10) UNSIGNED NOT NULL,
  `grant_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `scope_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- 表的结构 `oauth_refresh_tokens`
--

CREATE TABLE `oauth_refresh_tokens` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `access_token_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `expire_time` int(11) NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- --------------------------------------------------------

--
-- 表的结构 `oauth_scopes`
--

CREATE TABLE `oauth_scopes` (
  `id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `description` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_scopes`
--

INSERT INTO `oauth_scopes` (`id`, `description`, `created_at`, `updated_at`) VALUES
('scope1', '获得您的个人账号信息', '2016-10-08 03:23:35', '2016-10-08 03:23:38');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_sessions`
--

CREATE TABLE `oauth_sessions` (
  `id` int(10) UNSIGNED NOT NULL,
  `client_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `owner_type` enum('client','user') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'user',
  `owner_id` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `client_redirect_uri` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_sessions`
--

INSERT INTO `oauth_sessions` (`id`, `client_id`, `owner_type`, `owner_id`, `client_redirect_uri`, `created_at`, `updated_at`) VALUES
(1, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-07 23:45:32', '2016-10-07 23:45:32'),
(2, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 00:53:32', '2016-10-08 00:53:32'),
(3, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 00:55:39', '2016-10-08 00:55:39'),
(4, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 01:02:40', '2016-10-08 01:02:40'),
(5, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 01:03:46', '2016-10-08 01:03:46'),
(6, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 01:10:18', '2016-10-08 01:10:18'),
(7, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 01:12:55', '2016-10-08 01:12:55'),
(8, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 01:14:28', '2016-10-08 01:14:28'),
(9, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 02:22:42', '2016-10-08 02:22:42'),
(10, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 02:48:21', '2016-10-08 02:48:21'),
(11, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 02:48:24', '2016-10-08 02:48:24'),
(12, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 02:48:32', '2016-10-08 02:48:32'),
(13, 'crm2', 'user', '2', 'http://121.42.170.15:9494/callback', '2016-10-08 02:48:53', '2016-10-08 02:48:53'),
(14, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 16:55:12', '2016-10-08 16:55:12'),
(15, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 16:57:55', '2016-10-08 16:57:55'),
(16, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 17:53:10', '2016-10-08 17:53:10'),
(17, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 17:53:14', '2016-10-08 17:53:14'),
(18, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 17:54:27', '2016-10-08 17:54:27'),
(19, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 17:58:52', '2016-10-08 17:58:52'),
(20, 'crm2', 'user', '1', 'http://121.42.170.15:9494/callback', '2016-10-08 18:06:40', '2016-10-08 18:06:40');

-- --------------------------------------------------------

--
-- 表的结构 `oauth_session_scopes`
--

CREATE TABLE `oauth_session_scopes` (
  `id` int(10) UNSIGNED NOT NULL,
  `session_id` int(10) UNSIGNED NOT NULL,
  `scope_id` varchar(40) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NULL DEFAULT NULL,
  `updated_at` timestamp NULL DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- 转存表中的数据 `oauth_session_scopes`
--

INSERT INTO `oauth_session_scopes` (`id`, `session_id`, `scope_id`, `created_at`, `updated_at`) VALUES
(1, 1, 'scope1', '2016-10-07 23:45:32', '2016-10-07 23:45:32'),
(2, 2, 'scope1', '2016-10-08 00:53:32', '2016-10-08 00:53:32'),
(3, 3, 'scope1', '2016-10-08 00:55:39', '2016-10-08 00:55:39'),
(4, 4, 'scope1', '2016-10-08 01:02:40', '2016-10-08 01:02:40'),
(5, 5, 'scope1', '2016-10-08 01:03:46', '2016-10-08 01:03:46'),
(6, 6, 'scope1', '2016-10-08 01:10:18', '2016-10-08 01:10:18'),
(7, 7, 'scope1', '2016-10-08 01:12:55', '2016-10-08 01:12:55'),
(8, 8, 'scope1', '2016-10-08 01:14:28', '2016-10-08 01:14:28'),
(9, 9, 'scope1', '2016-10-08 02:22:42', '2016-10-08 02:22:42'),
(10, 10, 'scope1', '2016-10-08 02:48:21', '2016-10-08 02:48:21'),
(11, 11, 'scope1', '2016-10-08 02:48:24', '2016-10-08 02:48:24'),
(12, 12, 'scope1', '2016-10-08 02:48:32', '2016-10-08 02:48:32'),
(13, 13, 'scope1', '2016-10-08 02:48:53', '2016-10-08 02:48:53'),
(14, 14, 'scope1', '2016-10-08 16:55:12', '2016-10-08 16:55:12'),
(15, 15, 'scope1', '2016-10-08 16:57:55', '2016-10-08 16:57:55'),
(16, 16, 'scope1', '2016-10-08 17:53:10', '2016-10-08 17:53:10'),
(17, 17, 'scope1', '2016-10-08 17:53:14', '2016-10-08 17:53:14'),
(18, 18, 'scope1', '2016-10-08 17:54:27', '2016-10-08 17:54:27'),
(19, 19, 'scope1', '2016-10-08 17:58:52', '2016-10-08 17:58:52'),
(20, 20, 'scope1', '2016-10-08 18:06:40', '2016-10-08 18:06:40');

-- --------------------------------------------------------

--
-- 表的结构 `password_resets`
--

CREATE TABLE `password_resets` (
  `email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `token` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `oauth_access_tokens`
--
ALTER TABLE `oauth_access_tokens`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `oauth_access_tokens_id_session_id_unique` (`id`,`session_id`),
  ADD KEY `oauth_access_tokens_session_id_index` (`session_id`);

--
-- Indexes for table `oauth_access_token_scopes`
--
ALTER TABLE `oauth_access_token_scopes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_access_token_scopes_access_token_id_index` (`access_token_id`),
  ADD KEY `oauth_access_token_scopes_scope_id_index` (`scope_id`);

--
-- Indexes for table `oauth_auth_codes`
--
ALTER TABLE `oauth_auth_codes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_auth_codes_session_id_index` (`session_id`);

--
-- Indexes for table `oauth_auth_code_scopes`
--
ALTER TABLE `oauth_auth_code_scopes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_auth_code_scopes_auth_code_id_index` (`auth_code_id`),
  ADD KEY `oauth_auth_code_scopes_scope_id_index` (`scope_id`);

--
-- Indexes for table `oauth_clients`
--
ALTER TABLE `oauth_clients`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `oauth_clients_id_secret_unique` (`id`,`secret`);

--
-- Indexes for table `oauth_client_endpoints`
--
ALTER TABLE `oauth_client_endpoints`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `oauth_client_endpoints_client_id_redirect_uri_unique` (`client_id`,`redirect_uri`);

--
-- Indexes for table `oauth_client_grants`
--
ALTER TABLE `oauth_client_grants`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_client_grants_client_id_index` (`client_id`),
  ADD KEY `oauth_client_grants_grant_id_index` (`grant_id`);

--
-- Indexes for table `oauth_client_scopes`
--
ALTER TABLE `oauth_client_scopes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_client_scopes_client_id_index` (`client_id`),
  ADD KEY `oauth_client_scopes_scope_id_index` (`scope_id`);

--
-- Indexes for table `oauth_grants`
--
ALTER TABLE `oauth_grants`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `oauth_grant_scopes`
--
ALTER TABLE `oauth_grant_scopes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_grant_scopes_grant_id_index` (`grant_id`),
  ADD KEY `oauth_grant_scopes_scope_id_index` (`scope_id`);

--
-- Indexes for table `oauth_refresh_tokens`
--
ALTER TABLE `oauth_refresh_tokens`
  ADD PRIMARY KEY (`access_token_id`),
  ADD UNIQUE KEY `oauth_refresh_tokens_id_unique` (`id`);

--
-- Indexes for table `oauth_scopes`
--
ALTER TABLE `oauth_scopes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `oauth_sessions`
--
ALTER TABLE `oauth_sessions`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_sessions_client_id_owner_type_owner_id_index` (`client_id`,`owner_type`,`owner_id`);

--
-- Indexes for table `oauth_session_scopes`
--
ALTER TABLE `oauth_session_scopes`
  ADD PRIMARY KEY (`id`),
  ADD KEY `oauth_session_scopes_session_id_index` (`session_id`),
  ADD KEY `oauth_session_scopes_scope_id_index` (`scope_id`);

--
-- Indexes for table `password_resets`
--
ALTER TABLE `password_resets`
  ADD KEY `password_resets_email_index` (`email`),
  ADD KEY `password_resets_token_index` (`token`);

--
-- 在导出的表使用AUTO_INCREMENT
--

--
-- 使用表AUTO_INCREMENT `oauth_access_token_scopes`
--
ALTER TABLE `oauth_access_token_scopes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- 使用表AUTO_INCREMENT `oauth_auth_code_scopes`
--
ALTER TABLE `oauth_auth_code_scopes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- 使用表AUTO_INCREMENT `oauth_client_endpoints`
--
ALTER TABLE `oauth_client_endpoints`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- 使用表AUTO_INCREMENT `oauth_client_grants`
--
ALTER TABLE `oauth_client_grants`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- 使用表AUTO_INCREMENT `oauth_client_scopes`
--
ALTER TABLE `oauth_client_scopes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;
--
-- 使用表AUTO_INCREMENT `oauth_grant_scopes`
--
ALTER TABLE `oauth_grant_scopes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
--
-- 使用表AUTO_INCREMENT `oauth_sessions`
--
ALTER TABLE `oauth_sessions`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- 使用表AUTO_INCREMENT `oauth_session_scopes`
--
ALTER TABLE `oauth_session_scopes`
  MODIFY `id` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=21;
--
-- 限制导出的表
--

--
-- 限制表 `oauth_access_tokens`
--
ALTER TABLE `oauth_access_tokens`
  ADD CONSTRAINT `oauth_access_tokens_session_id_foreign` FOREIGN KEY (`session_id`) REFERENCES `oauth_sessions` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_access_token_scopes`
--
ALTER TABLE `oauth_access_token_scopes`
  ADD CONSTRAINT `oauth_access_token_scopes_access_token_id_foreign` FOREIGN KEY (`access_token_id`) REFERENCES `oauth_access_tokens` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `oauth_access_token_scopes_scope_id_foreign` FOREIGN KEY (`scope_id`) REFERENCES `oauth_scopes` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_auth_codes`
--
ALTER TABLE `oauth_auth_codes`
  ADD CONSTRAINT `oauth_auth_codes_session_id_foreign` FOREIGN KEY (`session_id`) REFERENCES `oauth_sessions` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_auth_code_scopes`
--
ALTER TABLE `oauth_auth_code_scopes`
  ADD CONSTRAINT `oauth_auth_code_scopes_auth_code_id_foreign` FOREIGN KEY (`auth_code_id`) REFERENCES `oauth_auth_codes` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `oauth_auth_code_scopes_scope_id_foreign` FOREIGN KEY (`scope_id`) REFERENCES `oauth_scopes` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_client_endpoints`
--
ALTER TABLE `oauth_client_endpoints`
  ADD CONSTRAINT `oauth_client_endpoints_client_id_foreign` FOREIGN KEY (`client_id`) REFERENCES `oauth_clients` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- 限制表 `oauth_client_grants`
--
ALTER TABLE `oauth_client_grants`
  ADD CONSTRAINT `oauth_client_grants_client_id_foreign` FOREIGN KEY (`client_id`) REFERENCES `oauth_clients` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `oauth_client_grants_grant_id_foreign` FOREIGN KEY (`grant_id`) REFERENCES `oauth_grants` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- 限制表 `oauth_client_scopes`
--
ALTER TABLE `oauth_client_scopes`
  ADD CONSTRAINT `oauth_client_scopes_client_id_foreign` FOREIGN KEY (`client_id`) REFERENCES `oauth_clients` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `oauth_client_scopes_scope_id_foreign` FOREIGN KEY (`scope_id`) REFERENCES `oauth_scopes` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_grant_scopes`
--
ALTER TABLE `oauth_grant_scopes`
  ADD CONSTRAINT `oauth_grant_scopes_grant_id_foreign` FOREIGN KEY (`grant_id`) REFERENCES `oauth_grants` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `oauth_grant_scopes_scope_id_foreign` FOREIGN KEY (`scope_id`) REFERENCES `oauth_scopes` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_refresh_tokens`
--
ALTER TABLE `oauth_refresh_tokens`
  ADD CONSTRAINT `oauth_refresh_tokens_access_token_id_foreign` FOREIGN KEY (`access_token_id`) REFERENCES `oauth_access_tokens` (`id`) ON DELETE CASCADE;

--
-- 限制表 `oauth_sessions`
--
ALTER TABLE `oauth_sessions`
  ADD CONSTRAINT `oauth_sessions_client_id_foreign` FOREIGN KEY (`client_id`) REFERENCES `oauth_clients` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- 限制表 `oauth_session_scopes`
--
ALTER TABLE `oauth_session_scopes`
  ADD CONSTRAINT `oauth_session_scopes_scope_id_foreign` FOREIGN KEY (`scope_id`) REFERENCES `oauth_scopes` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `oauth_session_scopes_session_id_foreign` FOREIGN KEY (`session_id`) REFERENCES `oauth_sessions` (`id`) ON DELETE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
