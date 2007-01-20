SAMPLES=SearchEvents.cs Authenticated.cs
MCSFLAGS= -debug -nologo -r:Mono.Upcoming.dll

EXTRA_DIST=$(SAMPLES)
CLEANFILES= *.exe *.mdb *.pdb *.dll

noinst_SCRIPTS=$(SAMPLES:.cs=.exe)

%.exe: %.cs Mono.Upcoming.dll
	$(MCS) $(MCSFLAGS) -out:$@ $<

Mono.Upcoming.dll: $(top_builddir)/src/Mono.Upcoming.dll
	cp $(top_builddir)/src/Mono.Upcoming.dll* .

