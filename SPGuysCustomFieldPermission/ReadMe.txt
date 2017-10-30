TODO
========================================================================================
1. Prava na View vo webparte:
--------------
V pripade ak pridavam list do webpartzone tak si vyberam ake view bude mat v tejto webpartzone. Lenze toto view nie je to na ktore je uz nastavene pravo 
lebo sa vytvara kopia tohto view. Teda mame view ktore je viditelne iba na tejto webpartzone ale v objekte currentList vo Views sa nachadza len je schovane.
Cize nefunguju prava na View ktore sa nachadza vo webpartzone

-------------
Vitvorim vizualnu settings webpartu ktoru bude mozne docasne pridat na tu stranku kde mas tie tie view. Nacitaju sa vsetky view na tej stranke
do tejto webparty a v nej sa bude dat nastavit pravo kedze tie view tam konkretne uvidim. Po vsetkych nastaveniach schovas tuto webpartu a kedze pravo si uz nastavil
tak vsetko po ulozeni bude fungovat a danne view sa schova. 




2. Alerty - Uz je to vyriesenie cez kod:
-----------------------------------------------------------------
Nasadenie alertov kvoli pravam sa musi robit cez PowerShell prikaz lebo moze byt problem s AccessDenied

stsadm -o updatealerttemplates -filename "C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\TEMPLATE\XML\SPGuys_AlertTemplates.xml" -url "http://intranet:80"


3. HttpModule - VSMENU.aspx
------------------------------------------------------------------
Ide o to ze teraz to nerobim pekne lebo sa to len kopiruje aj ked to osetrene bolo by lepsie to uplne nahradit cez httpmodule



4. Export To Excel, DataSheet View, GroupBy problem s view field, webservice
--------------------------------------------------------------------
Nejak skusit dorobit


5. Dorobit Event receiver pre readonly aby nam fungovalo napr. datasheet view ze iba readonly
-----------------------------------------------------------------------



6. Attachment schovat pripadne dalsie stlpce
--------------------------------------------------------------------------------------------




