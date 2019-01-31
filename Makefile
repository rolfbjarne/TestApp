default:
	@echo Default is to do nothing.

archive:
	@if test -z $(BUG); then echo "No bug # specified using BUG=..."; exit 1; fi
	@rm -f ../bug$(BUG).zip
	@git archive --out ../bug$(BUG).zip --prefix=bug$(BUG)/ HEAD
	@echo Created archive: ../bug$(BUG).zip
	@unzip -l ../bug$(BUG).zip

dev:
	/Library/Frameworks/Mono.framework/Commands/xbuild /p:Platform=iPhone
	/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/bin/mtouch --installdev bin/iPhone/Debug/*.app
	/Library/Frameworks/Xamarin.iOS.framework/Versions/Current/bin/mtouch --launchdev  $(shell /usr/libexec/PlistBuddy -c 'Print CFBundleIdentifier' bin/iPhone/Debug/*.app/Info.plist)

libtest.o: test.m Makefile
	clang -g -arch arm64_32 -c $< -o$@ -isysroot /Applications/Xcode101.app/Contents/Developer/Platforms/WatchOS.platform/Developer/SDKs/WatchOS.sdk -fembed-bitcode

