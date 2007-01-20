pkgconfigdir=$(libdir)/pkgconfig
MCSFLAGS= -debug -nologo -r:System.Web.dll

monoupcomingdir = $(prefix)/lib/Mono.Upcoming
pkgconfig_DATA = mono-upcoming.pc
monoupcoming_SCRIPTS = Mono.Upcoming.dll

CLEANFILES = Mono.Upcoming.dll*

#
monoupcoming_references=

monoupcoming_sources  = \
			Mono.Upcoming/AuthenticatedConnection.cs      \
			Mono.Upcoming/Category.cs      \
			Mono.Upcoming/ConnectionWrapper.cs      \
			Mono.Upcoming/Country.cs        \
			Mono.Upcoming/Event.cs  \
			Mono.Upcoming/Group.cs  \
			Mono.Upcoming/Metro.cs  \
			Mono.Upcoming/Response.cs       \
			Mono.Upcoming/State.cs  \
			Mono.Upcoming/Token.cs  \
			Mono.Upcoming/UpcomingConnection.cs     \
			Mono.Upcoming/UpcomingParam.cs  \
			Mono.Upcoming/UpcomingUtil.cs   \
			Mono.Upcoming/User.cs   \
			Mono.Upcoming/Venue.cs  \
			Mono.Upcoming/Watch.cs

monoupcoming_build_sources = $(addprefix $(srcdir)/, $(monoupcoming_sources))

Mono.Upcoming.dll: $(monoupcoming_build_sources)
	$(MCS) $(MCSFLAGS) $(monoupcoming_references) /target:library /out:$@ $(monoupcoming_build_sources)

