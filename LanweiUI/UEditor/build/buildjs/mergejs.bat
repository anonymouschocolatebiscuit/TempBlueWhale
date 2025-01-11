java -jar js.jar build_editor_all.js ../../ ../../editor_all.js

java -jar yuicompressor-2.4.6.jar --nomunge --preserve-semi  --disable-optimizations --charset utf-8 ../../editor_all.js -o ../../editor_all_min.js
