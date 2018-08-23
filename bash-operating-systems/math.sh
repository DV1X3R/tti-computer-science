#!/bin/bash

#let m=$1*$2
#let "m = $1 * $2"
m=$(echo "$1 * $2" | bc)
echo $1 \* $2 = $m

min=$1
max=$(expr $2 - $1 + 1)
echo Random between $1 and $2: $(expr $(expr $RANDOM % $max) + $min)
echo Random between 0 and 100: $(expr $RANDOM % 101)

echo Tomorrows date: $(date -v+1d +%Y-%m-%d)
