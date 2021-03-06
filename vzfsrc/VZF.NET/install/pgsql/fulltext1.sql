﻿-- This scripts for PostgreSQL Yet Another Forum https://github.com/vzrus/YetAnotherForumExtraDataLayers http://sourceforge.net/projects/yafdotnet/
-- were created by vzrus from vz-team  https://github.com/vzrus
-- They are distributed under terms of GPLv2 licence only as in http://www.fsf.org/licensing/licenses/gpl.html
-- Copyright vzrus(c) 2009-2012


CREATE TEXT SEARCH DICTIONARY english_stem (
    TEMPLATE = snowball,
    Language = english,
    StopWords = english
);
--GO
CREATE TEXT SEARCH DICTIONARY russian_stem (
    TEMPLATE = snowball,
    Language = russian,
    StopWords = russian
);
CREATE INDEX pgweb_idx ON pgweb USING gin(to_tsvector('english', title || ' ' || body));
ALTER TABLE pgweb ADD COLUMN textsearchable_index_col tsvector;

UPDATE pgweb SET textsearchable_index_col =
     to_tsvector('english', coalesce(title,'') || ' ' || coalesce(body,''));

CREATE INDEX textsearch_idx ON pgweb USING gin(textsearchable_index_col);

SELECT message
FROM yaf_message
WHERE to_tsvector(message) @@ to_tsquery('gsdgsdfg');

SELECT title
FROM pgweb
WHERE textsearchable_index_col @@ to_tsquery('create & table')
ORDER BY last_mod_date DESC
LIMIT 10;
When using a separate column to store the tsvector representation, it is necessary to create a trigger to keep the tsvector column current anytime title or body changes. Section 12.4.3 explains how to do that.

CREATE TABLE messages (
    title       text,
    body        text,
    tsv         tsvector
);

CREATE TRIGGER tsvectorupdate BEFORE INSERT OR UPDATE
ON messages FOR EACH ROW EXECUTE PROCEDURE
tsvector_update_trigger(tsv, 'pg_catalog.english', title, body);

INSERT INTO messages VALUES('title here', 'the body text is here');

SELECT * FROM messages;
   title    |         body          |            tsv             
------------+-----------------------+----------------------------
 title here | the body text is here | 'bodi':4 'text':5 'titl':1

SELECT title, body FROM messages WHERE tsv @@ to_tsquery('title & body');
   title    |         body          
------------+-----------------------
 title here | the body text is here

 A limitation of these built-in triggers is that they treat all the input columns alike. To process columns differently — for example, to weight title differently from body — it is necessary to write a custom trigger. Here is an example using PL/pgSQL as the trigger language
 CREATE FUNCTION messages_trigger() RETURNS trigger AS $$
begin
  new.tsv :=
     setweight(to_tsvector('pg_catalog.english', coalesce(new.title,'')), 'A') ||
     setweight(to_tsvector('pg_catalog.english', coalesce(new.body,'')), 'D');
  return new;
end
$$ LANGUAGE plpgsql;

CREATE TRIGGER tsvectorupdate BEFORE INSERT OR UPDATE
    ON messages FOR EACH ROW EXECUTE PROCEDURE messages_trigger();
Keep in mind that it is important to specify the configuration name explicitly when creating tsvector values inside triggers, so that the column's contents will not be affected by changes to default_text_search_config. Failure to do this is likely to lead to problems such as search results changing after a dump and reload.

UPDATE {databaseSchema}.{objectQualifier}message SET ts_message =
setweight( coalesce( to_tsvector(message),''),'A');


select title,rank_cd(fts, q) from apod, 
to_tsquery('supernovae & x-ray') q 
where fts  @@ q order by rank_cd desc limit 5;

create index fts_idx on apod using gin (fts);
