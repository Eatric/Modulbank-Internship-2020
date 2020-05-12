--
-- PostgreSQL database dump
--

-- Dumped from database version 12.2
-- Dumped by pg_dump version 12.2

-- Started on 2020-05-12 16:52:50

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE financeapp;
--
-- TOC entry 2846 (class 1262 OID 16393)
-- Name: financeapp; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE financeapp WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Russian_Russia.1251' LC_CTYPE = 'Russian_Russia.1251';


ALTER DATABASE financeapp OWNER TO postgres;

\connect financeapp

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 203 (class 1259 OID 24650)
-- Name: Accounts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Accounts" (
    "Number" character(10) DEFAULT '0'::bpchar NOT NULL,
    "Balance" money DEFAULT 0 NOT NULL,
    "Status" smallint DEFAULT 0 NOT NULL,
    "Owner" uuid NOT NULL
);


ALTER TABLE public."Accounts" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 24674)
-- Name: Transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Transactions" (
    "Id" bigint NOT NULL,
    "FromAccount" character(10) DEFAULT '0'::bpchar NOT NULL,
    "ToAccount" character(10) DEFAULT '0'::bpchar NOT NULL,
    "Amount" money DEFAULT 0 NOT NULL,
    "When" timestamp without time zone NOT NULL,
    "Type" smallint DEFAULT '0'::smallint NOT NULL
);


ALTER TABLE public."Transactions" OWNER TO postgres;

--
-- TOC entry 205 (class 1259 OID 24688)
-- Name: TransactionsAutoIncrement; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."TransactionsAutoIncrement"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
    CYCLE;


ALTER TABLE public."TransactionsAutoIncrement" OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 24589)
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Name" character varying(65) NOT NULL,
    "Email" character varying(65) NOT NULL,
    "Password" character varying(64) NOT NULL,
    "Salt" character varying(8) NOT NULL,
    "Photo" character varying(256) DEFAULT NULL::character varying,
    "Status" smallint DEFAULT 0 NOT NULL
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- TOC entry 2838 (class 0 OID 24650)
-- Dependencies: 203
-- Data for Name: Accounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Accounts" ("Number", "Balance", "Status", "Owner") VALUES ('4294562741', '181,00 ?', 0, 'c0126b81-2802-4a8a-85d4-aff7e896db32');
INSERT INTO public."Accounts" ("Number", "Balance", "Status", "Owner") VALUES ('4123456789', '120,50 ?', 0, '08960050-3365-439d-9d33-e2ba3fa5a669');


--
-- TOC entry 2839 (class 0 OID 24674)
-- Dependencies: 204
-- Data for Name: Transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (1, '1         ', '0         ', '0,00 ?', '2020-05-08 08:00:36.85763', 0);
INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (2, '2         ', '0         ', '0,00 ?', '2020-05-08 08:00:42.475828', 0);
INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (3, '1         ', '0         ', '0,00 ?', '2020-05-08 08:05:27.559732', 0);
INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (7, '2         ', '0         ', '0,00 ?', '2020-05-08 08:05:27.559732', 0);
INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (14, '1         ', '2         ', '0,00 ?', '2020-05-09 16:32:52', 0);
INSERT INTO public."Transactions" ("Id", "FromAccount", "ToAccount", "Amount", "When", "Type") VALUES (20, '4294562741', '4123456789', '10,00 ?', '2020-05-09 16:35:15.637717', 0);


--
-- TOC entry 2837 (class 0 OID 24589)
-- Dependencies: 202
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('c0126b81-2802-4a8a-85d4-aff7e896db32', 'Alexey', '123@gmail.com', 'A55870816298BD40123008CF08BFA6A17B0D4E7F9A80E22B9F190BABF6CF6CAB', 'c0126b81', NULL, 0);
INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('08960050-3365-439d-9d33-e2ba3fa5a669', 'Alexey', 'test@gmail.com', '1E047C6C4FC599B0B7E25CC6D5468933BB631EBDF0E6ED9BBA9262931BD13D39', '6babf2dc', '', 0);
INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('858e0b32-93cb-4309-9624-cb0792f53667', 'Alexey', 'secondTest@gmail.com', '1DF84D187533BCC165C366C2CE846EBFF89851105EC10946A25BE0757ED36093', '9693bd84', '', 1);


--
-- TOC entry 2847 (class 0 OID 0)
-- Dependencies: 205
-- Name: TransactionsAutoIncrement; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."TransactionsAutoIncrement"', 20, true);


--
-- TOC entry 2707 (class 2606 OID 24665)
-- Name: Accounts Accounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Accounts"
    ADD CONSTRAINT "Accounts_pkey" PRIMARY KEY ("Number");


--
-- TOC entry 2709 (class 2606 OID 24702)
-- Name: Transactions Transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transactions"
    ADD CONSTRAINT "Transactions_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2705 (class 2606 OID 24595)
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2710 (class 2606 OID 24658)
-- Name: Accounts FK_Accounts_Users; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Accounts"
    ADD CONSTRAINT "FK_Accounts_Users" FOREIGN KEY ("Owner") REFERENCES public."Users"("Id");


-- Completed on 2020-05-12 16:52:51

--
-- PostgreSQL database dump complete
--

