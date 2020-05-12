--
-- PostgreSQL database dump
--

-- Dumped from database version 12.2
-- Dumped by pg_dump version 12.2

-- Started on 2020-05-06 23:38:44

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

--
-- TOC entry 2847 (class 1262 OID 16393)
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

--
-- TOC entry 3 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO postgres;

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
-- TOC entry 205 (class 1259 OID 24674)
-- Name: Transactions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Transactions" (
    "Id" bigint NOT NULL,
    "FromAccount" character(10) DEFAULT '0'::bpchar NOT NULL,
    "ToAccount" character(10) DEFAULT '0'::bpchar NOT NULL,
    "Amount" money DEFAULT 0 NOT NULL,
    "When" timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public."Transactions" OWNER TO postgres;

--
-- TOC entry 204 (class 1259 OID 24672)
-- Name: Transactions_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Transactions_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Transactions_Id_seq" OWNER TO postgres;

--
-- TOC entry 2848 (class 0 OID 0)
-- Dependencies: 204
-- Name: Transactions_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Transactions_Id_seq" OWNED BY public."Transactions"."Id";


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
-- TOC entry 2700 (class 2604 OID 24677)
-- Name: Transactions Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transactions" ALTER COLUMN "Id" SET DEFAULT nextval('public."Transactions_Id_seq"'::regclass);


--
-- TOC entry 2839 (class 0 OID 24650)
-- Dependencies: 203
-- Data for Name: Accounts; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Accounts" ("Number", "Balance", "Status", "Owner") VALUES ('4294562741', '0,00 ?', 0, 'c0126b81-2802-4a8a-85d4-aff7e896db32');
INSERT INTO public."Accounts" ("Number", "Balance", "Status", "Owner") VALUES ('4123456789', '100,50 ?', 0, '08960050-3365-439d-9d33-e2ba3fa5a669');


--
-- TOC entry 2841 (class 0 OID 24674)
-- Dependencies: 205
-- Data for Name: Transactions; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 2838 (class 0 OID 24589)
-- Dependencies: 202
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('c0126b81-2802-4a8a-85d4-aff7e896db32', 'Alexey', '123@gmail.com', 'A55870816298BD40123008CF08BFA6A17B0D4E7F9A80E22B9F190BABF6CF6CAB', 'c0126b81', NULL, 0);
INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('08960050-3365-439d-9d33-e2ba3fa5a669', 'Alexey', 'test@gmail.com', '1E047C6C4FC599B0B7E25CC6D5468933BB631EBDF0E6ED9BBA9262931BD13D39', '6babf2dc', '', 0);
INSERT INTO public."Users" ("Id", "Name", "Email", "Password", "Salt", "Photo", "Status") VALUES ('858e0b32-93cb-4309-9624-cb0792f53667', 'Alexey', 'secondTest@gmail.com', '1DF84D187533BCC165C366C2CE846EBFF89851105EC10946A25BE0757ED36093', '9693bd84', '', 1);


--
-- TOC entry 2849 (class 0 OID 0)
-- Dependencies: 204
-- Name: Transactions_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Transactions_Id_seq"', 1, false);


--
-- TOC entry 2708 (class 2606 OID 24665)
-- Name: Accounts Accounts_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Accounts"
    ADD CONSTRAINT "Accounts_pkey" PRIMARY KEY ("Number");


--
-- TOC entry 2710 (class 2606 OID 24683)
-- Name: Transactions Transactions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Transactions"
    ADD CONSTRAINT "Transactions_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2706 (class 2606 OID 24595)
-- Name: Users Users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "Users_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2711 (class 2606 OID 24658)
-- Name: Accounts FK_Accounts_Users; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Accounts"
    ADD CONSTRAINT "FK_Accounts_Users" FOREIGN KEY ("Owner") REFERENCES public."Users"("Id");


-- Completed on 2020-05-06 23:38:45

--
-- PostgreSQL database dump complete
--

