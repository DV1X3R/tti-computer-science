#!/bin/bash

echo File: $1
echo Exists: $([ -e $1 ] && echo yes || echo no)
echo Directory: $([ -d $1 ] && echo yes || echo no)
echo Empty: $(! [ -s $1 ] && echo yes || echo no)
echo Read: $([ -r $1 ] && echo yes || echo no)
echo Write: $([ -w $1 ] && echo yes || echo no)
echo Execute: $([ -x $1 ] && echo yes || echo no)
