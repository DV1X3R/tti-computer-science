#!/bin/bash

echo $# arguments passed: $@
echo 1: $1 2:$2
#echo Script name: $0
#echo User: $USER Hostname: $HOSTNAME
#echo Seconds: $SECONDS Random: $RANDOM Line: $LINENO

words='/usr/share/dict/words'
reg5='^\w{5}$'
count=$(wc -l < $words)
reg5_count=$(cat $words | egrep $reg5 | wc -l)

echo Random word: $(cat $words | head -$(jot -r 1 1 $count) | tail -1)
echo Random 5 char word: $(cat $words | egrep $reg5 | head -$(jot -r 1 1 $reg5_count) | tail -1)

touch $1_$(date '+%Y-%m-%d').txt
