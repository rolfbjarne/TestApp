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

libtestcode.o: libtestcode.dev.o libtestcode.sim.o
	lipo -create -output $@ $?

libtestcode.dev.o: testcode.m Makefile
	clang -arch arm64 -c $< -o$@ -mios-simulator-version-min=6.0 -isysroot /Applications/Xcode10.app/Contents/Developer/Platforms/iPhoneOS.platform/Developer/SDKs/iPhoneOS.sdk

libtestcode.sim.o: testcode.m Makefile
	clang -arch x86_64 -c $< -o$@ -miphoneos-version-min=6.0 -isysroot /Applications/Xcode10.app/Contents/Developer/Platforms/iPhoneSimulator.platform/Developer/SDKs/iPhoneSimulator.sdk