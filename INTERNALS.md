# TCGSync (programátorská dokumentace)

*TCGSync je aplikace pro uživatele operačního systému Windows 10. Je napsána v .Net Frameworku 4.5.2 v jazyce C#. Aplikace synchronizuje kalendáře firemního systému Time Cockpit a Googlu. Jde o vícevláknovou aplikaci, neboť synchronizační procesy neprobíhají v hlavním vlákně (kvůli plynulosti aplikace).*

## Hlavní funkce

Jak již bylo zmíněno v předchozím odstavci, hlavní funkcí TCGSync je synchronizace událostí (Events/Timesheets) mezi uživatelskými účty Time Cockpitu a Google. Synchronizace je automatická (defaultně nastavená na každých 15 minut), zároveň však uživatel může vyvolat synchronizaci sám. Aplikace podporuje synchronizaci účtů více uživatelů.

## Soubory

Solution se skládá ze 4 projektů:

+ GoogleCalendarCommunication 

    Ten se stará o komunikaci s uživatelským účtem v Google Calendar
obsahuje následují soubory s kódem:
    + GBrooker.cs (zprostředkovává práci s událostmi v Google Calendar)
    + GUtils.cs (Jiné operace s Google Calendar)

+ TimeCockpitCommunication

    Ten se stará o komunikaci s uživatelským účtem v Time Cockpitu
obsahuje následují soubory s kódem:
    + TCBrooker.cs (zprostředkovává práci s událostmi v Time Cockpitu)
    + TCUtils.cs (Jiné práce s Google Calendar)
    + TCCredentialManager.cs (Ukládání uživatelských údajů z TimeCockpitu)

+ TCGSync.Entities

    Tento projekt definuje potřebné třídy používané v celé aplikaci
obsahuje následují soubory s kódem:
    + Event.cs (Třída využívaná pro události)
    + IBrooker.cs (Interface pro zprostředkovávání práce s událostmi)
    + User.cs (Třída využívaná pro uživatele)

+ TCGSync

    Hlavní projekt. Stará se o synchronizaci a UI
obsahuje následují soubory s kódem:
    + DataDatabase.cs (Statická třída obsahující všechna data)
    + EditUserForm.cs (Windows Form pro úpravu uživatele)
    + MainForm.cs (Hlavní okno aplikace)
    + NewUserForm.cs (Okno pro vytvoření uživatele)
    + Run.cs (Start aplikace)
    + Synchronisation.cs (Třída, která se stará o synchronizaci účtů)
    + SyncInfoGiver.cs (Třída, která se stará o zobrazení informací o synchronizaci)
    + UserCreator.cs (Třída, která se stará o vytvoření uživatele)
    + UserDeleter.cs (Třída, která se stará o smazání uživatele)
    + UserEditor.cs (Třída, která se stará o úpravu uživatele)

## Použité knihovny

V GoogleCalendarCommunication jsou použity knihovny pro práci s autentifikací a prací v Google Calendar:
+ Google.Apis
+ Google.Apis.Auth
+ Google.Apis.Calendar.v3
+ Google.Apis.Core

Pro práci s Time Cockpitem jsem použil protokol Open Data, který mi umožnil jednoduše komunikovat s Time Cockpitem přes RESTful API.

Dále je použita knihovna *Meziantou.Framework.Win32.CredentialManager*, která se stará o ukládání credentials z účtu Time Cockpit.

## Popsání jednotlivých částí dle namespace

### TCGSync

Jde o základní namespace celé aplikace a obsahuje její hlavní logiku.

Jeho hlavní části jsou statická třída DataDatabse, statická třída Synchronisation a statická třída *SyncInfoGiver*.

**Statická třída DataDatabase**

Jedná s třídu obsahující všechna data aplikace, tedy všechna data o uživatelích a jejich událostí a data o nastavení aplikace. Obsahuje také metody, které s daty pracují. Metoda *RefreshListBox()* zobrazuje uživatele v hlavním okně, metoda *SaveChanges()* je zase ukládá do souboru.

**Statická třída Synchronisation.**

Tato třída se stará o synchronizaci účtů všech uživatelů. Hlavní metodou této funkce je metoda *Sync()*, která vezme všechny uživatele a pro každého zvlášť synchronizuje jejich účty. Funkce, které jsou veřejné a obsahují ji jsou *SyncNow()*, která spustí synchronizaci hned, a funkce *AutoSync()*, která spustí časovač, který funkci spouští v pravidelných intervalech.

**Statická třída SyncInfoGiver**

Tato třída se stará o zobrazení informací ohledně synchronizace. Třida má datové položky *SyncInfo, MinutesToNextSync, IsProccessingSync, WasSyncStop, WasLastSyncSuccessful, ErrorMessage*, které když se aktualizují, tak se zavolá funkce SendMessage, která z datových položek vytvoří zprávu a tu pak pomocí metody *ShowMesagge()* pošle do Zobrazovacího řádku v hlavním okně. Tato třída zároveň obsahuje Timer, který po každé minutě aktualizuje čas, který zbývá do další automatické synchronizaci.

### TCGSync.UI

Jde o namespace obsahující všechny okna aplikace, tedy *EditUSerForm*, *MainForm* a *NewUserForm*.

**EditUserForm**

Pomáhá při komunikaci s uživatel při úpravě svých údajů.

**NewUserForm**

Pomáhá při komunikaci s uživatel při jeho vytváření.

**MainForm**

Jde o hlavní okno aplikace a ikonu tray.

### TCGSync.Modifications

Jedná se o namespace, který obsahuje třídu *UserCreator*, třídu *UserEditor* a třídu *UserDeleter*, které se starají o modifikací uživatelů.

**Třída UserCreator**

Tato třída vytváří uživatele. Nejdříve pomocí moteody *TCVerify()* zkontroluje, zda jsou Time Cockpit credentials korektní. Metodou *GoogleLogin()* získá uživatelovi Google credentials a metodou *GetUser()* vrací nového uživatele.

**Třída UserCreator**

Tato třída mění uživatele. Hlavní metodou zde je *ChangeUserInDatabase()*, která pozmění informace u daného uživatele v *DataDatabase.userDatabase*.

**Třída UserDeleter**

Tato velmi jednoduchá třída vymaže uživatele z *DataDatabase.userDatabase* pomocí metody *DeleteUser()*

## TCGSync.Entities

Tento namespace obsahuje základní entitami, s kterými se pak v aplikaci pracuje. Obahuje třídu *Event*, která nese data o jedné události, interface *IBrooker*, kterým je definována komunikace s TimeCockpitem a Google Calendar, a třídu *User*, jejíž instance nesou informace o daném uživateli.

### GoogleCalendarCommunication

Tento namespace má na starosti komunikaci s Time Cockpitem. Obsahuje třídu *GBrooker* a statickou třídu *GUtils*.

**Třída GBrooker**

Tato třída zprostředkovává práci s událostmi v Google Calendar. Pomocí metody *GetEvent(start, end)* vrací IEnumerable událostí, metodou *CreateEvent(Event)* vytvoří na Google Calendar účtu novou událost a pomocí *EditEvent(Event)* upraví na účtu událost se stejným ID.

**Statická třída GUtils**

Tato třída Komunikuje s Google Calendar a řeší vše, co se netýká událostí. Pomocí funkce *GLogin(user)*, lze uživatele přihlásit do Googlu, pomocí *GetCredentials(user)* lze získat uživatelovi credentials, pomocí *RemoveGoogleToken(user)* se odstraní google token. *GetCalendars(user)* vrátí všechny uživatelovi kalendáře a *GetEmail(user)* zjistí jeho emailvou adresu.

### TimeCockpitCommunication

Tento namespace má na starosti komunikaci s Time Cockpitem. Obsahuje třídu TCBrooker, statickou třídu TCUtils a statickou třídu TCCredentialsManager

**Třída TCBrooker**

Tato třída zprostředkovává práci s událostmi v Time Cockpit. Pomocí metody *GetEvent(start, end)* vrací IEnumerable událostí, metodou *CreateEvent(Event)* vytvoří na Time Cockpit účtě novou událost a pomocí *EditEvent(Event)* upraví na účtě událost se stejným ID.

**Statická třída TCUtils**

Tato třída komunikuje s Time Cockpitem a řeší vše, co se netýká událostí. Pomocí metody *TCVerify(username, password)* se zkontroluje korektnost údajů a metoda *GetFullName(user)* vrací celé jméno uživatele.

**Statická třída TCCredentialsManager**

Tato třída pomocí knihovny *Meziantou.Framework.Win32.CredentialManager* dokáže ukládat, číst a mazat TimeCokpit credentials v Credentials Manager, který obsahuje operační systém Windows 10.

## Problémy, které se v programu řeší

**Ukládání dat a jejich šifrování**

Aby se nemuseli po každé uživatelé přihlašovat bylo důležité vymyslet systém ukládání dat. Nejdříve jsem ukládal pouze interval synchronizace a data o všech uživatelích, kde se u událostí ukládali pouze jejich ID (vždy 2). Nicméně jsem nakonec musel ukládat úplně všechny data o událostech, abych mohl kontrolovat, zda se od poslední synchronizace něco změnilo.

Data jsou jednoduše uložené v souboru "data", kde první řádek značí interval synchronizace a každý další řádek je záznam o jednom uživateli, kde jsou parametry odděleny v programu nadefinovanými separátory. Problémem trochu bylo, že Description může obsahovat všechny možné znaky tedy i separátory. Proto jsem vybral takové separátory, které je možné poměrně lehce nahradit (například závorky jinými závorkami) a před ukládáním kontroluji a nahrazuji všechny znaky, které jsou stejné jako separátory v Description podobnými znaky.

Dalším problémem při ukládání bylo to, že heslo do Time Cockpitu je poměrně citlivý údaj, který by neměl být ukládán jen tak nezašifrovaně. Tento problém byl vyřešen tak, že heslo se vůbec neukládá, ale nachází se pouze ve Credential Manageru, který je obsažen ve Windows 10.

**Mazání starých událostí**

Pokud bych nemazal staré události mohl časem vzniknout zbytečně obrovský soubor s úplně zbytečnými daty. Zároveň můj program dovoluje v průběhu pozměňovat údaj o tom, kolik dnů do minulosti se mají data synchronizovat. Při vhodné kombinaci změn tohoto údaje a mazání starších událost, než je tento interval, by mohlo dojít k tomu, že se některé události v kalendářích zduplikují. Problém jsem vyřešil tak, že jsem nastavil maximální hodnotu tohoto údaje, tedy maximální dobu, kde událost ještě může být synchronizována. Události starší, než je tato doba, je možné smazat, aniž by docházelo k nějakým problémům. Mazání probíhá tak, že při načítání vpouštím jen události novější, než je maximální doba a při dalším uložení změn už se uloží jen novější události.

**Multithreading**

Aby UI fungovalo pro uživatele plynule, bylo vhodné některé procesy dělat v jiném vlákně než v hlavním.

Tím nejdůležitějším procesem, který běží v jiném vlákně je automatická synchronizace. K tomu jsem využil třídu *System.Timers.Timer*, která v daném časovém intervalu posílá vloženou metodu do Threadpoolu. Vzhledem k tomu, že pro uživatele není důležité, aby automatická synchronizace běžela v přesně definovaný čas, přišlo mi, že se na tento problém hodí threadpool nejvíce.

Dalším procesem je okamžitá synchronizace. Kdyby byla tato synchronizace v hlavním vlákně, hlavní okno by se "seklo" do doby, než by byla dokončená. U takového procesu však už je důležitá, co nejdřívější odezva. Proto jsem zde použil jiné vlákno (třídy *Thread*), které bude mít větší prioritu než Task.

Posledním procesem bylo loginování do Googlu. Zde byla motivace pro jiné vlákno trochu jiná. Problém byl v tom, že když se nechtěně zavře nebo nedokončí loginování aplikace stále čeká, než bude loginování dokončené, i když už to není možné. Pro jsem zvolil pro loginování jiné vlákno, neboť v případě, že se loginování selže, tak je možné spustit nový proces loginování. Samozřejmě co je vytvořen/upraven uživatel jsou všechny nezdařilé loginování abortováno.

**Threadsave a Windows.Form při více vláknech.**

Abych zajistil threadsave využil jsem funkce tzn zámků. Zámek jsem použil na následující:

Databázi uživatelů. Zde totiž bylo možné, že se při synchronizaci bude možné měnit či vytvářet nový uživatel.

Ukládání do souboru. Jelikož třída *StremWriter/StreamReader* není thread save a mohlo docházek ukládání z více vláken najednou opatřil jsem proces ukládání a čtení ze souborové databáze zámkem *FileDatabaseLocker*.

Interval synchronizace. Tato položka může být změněna, zatímco se provádí synchronizace. Proto jsem použil zámek *IntervalInMinutesLocker*.

SyncInfoGiver. Jelikož data o synchronizaci do této třídy zasílá sama synchronizace, která mlže být z více vláken opatřil jsem ji také zámkem *SyncInfoGiverLocker*, který je použit při každém vstupu do datových položek SyncInfoGiver.

Problém s multithreadingem nastal také když jsem chtěl zobrazovat data o synchronizaci. Problémem bylo, že Widows.Form nepodporuje přístup z jiného vlákna než z toho, z kterého byl Form vytvořen. Dá se to však obejít přes funkci Invoke, která si daný přístup z jiného vlákna zařadí do své fronty a až na něj přijde řada tak ho provede. Jelikož mi zde malé odchylky od časové přesnosti nevadili, mohl jsem funkci Invoke použít.

**Změny tokenů**

Funkce, která loginuje uživatele na jeho google účet, se nejprve koukne zda v dané složce neexistuje token s daným jménem a pokud ne tak ho požádá o údaje a token vytvoří. Nelze tedy měnit token ve složce tokenů, neboť se ho metoda snaží najít a nelze ho tedy přepsat. Zároveň se nemůže dřívější token smazat, neboť změny ještě nebyly potvrzeny. Nejjednodušší způsob se pro mě tedy jevil, token dát do jiné dočasné složky a poté ho po potvrzení změn dosadit za pravý token (při zamčené databázi).

**Google kalendáře**

Google účet má možnosti mít více kalendářů. Uživatelé si však určitě nepřejí, aby všechny jejich události byli synchronizované s TimeCokpitem (například narozeniny). Proto při vytváření/úpravě kalendáře si musí uživatel zvolit, s kterým kalendářem se má Time Cokpit synchronizovat, případně vytvořit nový.

**GetGoogleEmail()**

Z loginování na Google účet lze získat pouze token, ale už ne credentials. Proto nebylo jednoduché Google email získat. Email je nakonec získán jako jméno primárního kalendáře uživatele, které by mělo být u všech uživatelů nastaveno jako jejich emailová adresa.
