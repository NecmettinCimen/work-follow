

create database dbodevtakip

create table Kullanici(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
Ad varchar(50),
Soyad varchar(50),
Cinsiyet varchar(50),
Telefon varchar(15),
Eposta varchar(50),
Sifre varchar(50),
Unvani varchar(50),
Egitim varchar(50),
Sirket varchar(50),
primary key (Id));




create table Grup(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
Ad varchar(50),
Aciklama varchar(250),
primary key (Id),
YoneticiId int references kullanici(Id));

create table KullaniciGrup(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
primary key (Id),
KullaniciId int references kullanici(Id),
GrupId int references grup(Id));


create table Durum(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
Ad varchar(50),
primary key (Id));


create table Kategori(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
Ad varchar(50),
primary key (Id));


create table Proje(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
Ad varchar(50),
Aciklama varchar(250),
DurumId int references durum(Id),
KategoriId int references kategori(Id),
BaslangicTarihi date,
BitisTarihi date,
YoneticiId int references kullanici(Id),
GrupId int references grup(Id));


create table Etkinlik(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
Baslik varchar(50),
Aciklama varchar(250),
DurumId int references durum(Id),
KategoriId int references kategori(Id),
BaslangicTarihi date,
BitisTarihi date,
AtananId int references kullanici(Id),
ProjeId int references proje(Id));

create table HareketTip(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
Ad varchar(50),
Aciklama varchar(250));


create table EtkinlikHareket(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
primary key (Id),
HareketTipId int references harekettip(Id),
EtkinlikId int references etkinlik(Id));


create table TblNot(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
Konu varchar(50),
Aciklama varchar(50));


create table Dokuman(
Id serial,
Aktif bool not null default true,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
Ad varchar(50),
Tip varchar(50),
Boyut int,
DokumanData bytea);

create table KullaniciDokuman(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
KullaniciId int references kullanici(id),
DokumanId int references dokuman(Id));


create table ProjeDokuman(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
ProjeId int references proje(id),
DokumanId int references dokuman(Id));


create table ProjeNot(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
ProjeId int references proje(id),
NotId int references tblnot(Id));

create table KullaniciNot(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
GuncellemeTarihi date,
GuncelleyenKisi int,
primary key (Id),
KullaniciId int references kullanici(id),
NotId int references tblnot(Id));


create table ProjeHareket(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
primary key (Id),
HareketTipId int references harekettip(Id),
ProjeId int references proje(Id));


create table SurekliEtkinlik(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int,
primary key (Id),
EtkinlikId int references etkinlik(Id));


create table KullaniciAyarlar(
Id serial,
Sil bool not null default false,
OlusturmaTarihi date not null default now(),
OlusturanKisi int references Kullanici(Id),
primary key (Id),
Ayar JSON not null);
