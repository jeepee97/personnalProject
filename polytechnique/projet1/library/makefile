


# Nom de votre projet
# (utilisez un seul mot, exemple: 'monprojet')
PROJECTNAME=libMagic
# Fichiers sources
# Utilisez le suffixe .cpp pour les fichiers C++
# Listez tous les fichiers a compiler, separes par
# un espace. exemple: 'tp1.c tp2.cpp':
PRJSRC= $(wildcard *.cpp)

# Inclusions additionnels (ex: -I/path/to/mydir)
INC= 

# Libraires a lier (ex: -lmylib)
LIBS=

# Niveau d'optimization
# Utilisez s (size opt), 1, 2, 3 ou 0 (off)
OPTLEVEL=s



####### Cible (Target) #######

#Nom des cibles par defaut
TRG=$(PROJECTNAME).a

include ../Makefile.commun


# Implementation de la cible
$(TRG): $(OBJDEPS)
	ar crs $(TRG) $(OBJDEPS)
library:
	echo "skip"
