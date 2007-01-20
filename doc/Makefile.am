if ENABLE_MONODOC
SOURCESDIR=$(libdir)/monodoc/sources
TARGETS=Mono.Upcoming.tree Mono.Upcoming.zip Mono.Upcoming.source
UPDATER = $(MONODOCER) -path:Mono.Upcoming $(UPDATE_ASSEMBLIES)
else
SOURCESDIR=$(datadir)
TARGETS=
UPDATER = echo "Documentation build is disabled."
endif

NAMESPACES=Mono.Upcoming
UPDATE_ASSEMBLIES = -assembly:$(top_builddir)/src/Mono.Upcoming.dll

docsdir=$(SOURCESDIR)
docs_DATA=$(TARGETS)

CLEANFILES=Mono.Upcoming.tree Mono.Upcoming.zip
EXTRA_DIST=Mono.Upcoming.source

Mono.Upcoming: Mono.Upcoming.tree

Mono.Upcoming.tree Mono.Upcoming.zip: $(srcdir)/Mono.Upcoming/*.xml $(srcdir)/Mono.Upcoming/*/*.xml
	mdassembler --ecma $(srcdir)/Mono.Upcoming -o Mono.Upcoming

install-hook:
	cp Mono.Upcoming.* `pkg-config monodoc --variable=sourcesdir`

uninstall-hook:
	rm -f `pkg-config monodoc --variable=sourcesdir`/Mono.Upcoming.*

update: 
	$(UPDATER)

update-delete: 
	$(UPDATER) --delete

merge:
	monodoc --merge-changes ~/.config/monodoc/changeset.xml .

dist-hook:
	mkdir -p $(distdir)/Mono.Upcoming
	cp $(srcdir)/Mono.Upcoming/*.xml $(distdir)/Mono.Upcoming/
	for i in $(NAMESPACES); do				\
		mkdir -p $(distdir)/Mono.Upcoming/$$i;			\
		cp $(srcdir)/Mono.Upcoming/$$i/*.xml $(distdir)/Mono.Upcoming/$$i;	\
	done

