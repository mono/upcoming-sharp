dnl configure.ac for Mono.Upcoming library
AC_PREREQ(2.57)
AC_INIT(src/Mono.Upcoming/UpcomingConnection.cs)
AC_CANONICAL_SYSTEM
AM_INIT_AUTOMAKE(Mono.Upcoming, 0.1.0)
AM_MAINTAINER_MODE

AC_PROG_INSTALL

AC_PATH_PROG(CSC, csc, no)
AC_PATH_PROG(MCS, mcs, no)
AC_PATH_PROG(RUNTIME, mono, no)
CS="C#"
if test "x$CSC" = "xno" -a "x$MCS" = "xno"  ; then
	dnl AC_MSG_ERROR([You need to install a C# compiler])
	AC_MSG_ERROR([No $CS compiler found])
fi

if test "x$MCS" = "xno" ; then
	MCS=$CSC
fi


AC_PATH_PROG(MDASSEMBLER, mdassembler, no)
AC_PATH_PROG(MONODOCER, monodocer, no)
if test "x$MONODOCER" = "xno" -o "x$MDASSEMBLER" = "xno"; then
  enable_monodoc=no
  doc_sources_dir=
else
  enable_monodoc=yes
  doc_sources_dir="`pkg-config --variable=sourcesdir monodoc`"
fi

AM_CONDITIONAL(ENABLE_MONODOC, test "x$enable_monodoc" = "xyes")
AC_SUBST(MCS)
AC_SUBST(RUNTIME)
AC_SUBST(MDASSEMBLER)
AC_SUBST(MONODOCER)
AC_OUTPUT([
Makefile
src/Makefile
src/mono-upcoming.pc
src/Mono.Upcoming/AssemblyInfo.cs
doc/Makefile
examples/Makefile
])

echo "   * $CS compiler: $MCS"
echo "   * Documentation build enabled: $enable_monodoc "
if test "x$enable_monodoc" = "xyes" -a "x$doc_sources_dir" != "x$prefix/lib/monodoc/sources"; then
  echo "      WARNING: The install prefix is different than the monodoc prefix."
  echo "               Monodoc will not be able to load the documentation."
fi

