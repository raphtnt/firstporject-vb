 ---------------------------------------------------------------
 --        Script Oracle.
 ---------------------------------------------------------------


------------------------------------------------------------
-- Table: TABLE1
------------------------------------------------------------
CREATE TABLE TABLE1(
	id         NUMBER NOT NULL ,
	firstname  VARCHAR2 (100) NOT NULL  ,
	lastname   VARCHAR2 (100) NOT NULL  ,
	pays       VARCHAR2 (100) NOT NULL  ,
	ville      VARCHAR2 (100) NOT NULL  ,
	CONSTRAINT TABLE1_PK PRIMARY KEY (id)
);





CREATE SEQUENCE Seq_TABLE1_id START WITH 1 INCREMENT BY 1 NOCYCLE;


CREATE OR REPLACE TRIGGER TABLE1_id
	BEFORE INSERT ON TABLE1
  FOR EACH ROW
	WHEN (NEW.id IS NULL)
	BEGIN
		 select Seq_TABLE1_id.NEXTVAL INTO :NEW.id from DUAL;
	END;


