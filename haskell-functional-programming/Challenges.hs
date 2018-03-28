-- • Reverse numbers in a natural number 1234 -> [4, 3, 2, 1]
reverse_ :: Integer -> [Integer]
reverse_ 0 = []
reverse_ x = mod x 10 : reverse_ (div x 10)

-- • Check if a word is a palindrome. “ABBA” -> True; “Queen” -> False
palindrome :: [Char] -> Bool
palindrome [] = True
palindrome x
  | head x == last x = palindrome (init (tail x))
  | otherwise = False

-- • Find a maximum in the list. [1, 10, 9, 7, 15, 3] -> 15
max_ :: [Integer] -> Integer
max_ x
  | head x > last x = max_ (init x)
  | last x > head x = max_ (tail x)
  | otherwise = x !! 0

-- • A natural number is given. Function should return True if a given number
-- is a power of two and False if it’s not. 2 -> True; 14 -> False; 16 -> True
pot :: Integer -> Bool
pot x
  | (x > 1) && (mod x 2) == 0 = pot (div x 2)
  | x == 1 = True
  | otherwise = False


-- Implement own versions of:
-- • zipWith
zipWith_ :: (a -> a -> a) -> [a] -> [a] -> [a]
zipWith_ _ [] [] = []
zipWith_ func (x:xs) (z:zs) = func x z : zipWith_ func xs zs

-- • flip
flip_ :: (a -> a -> a) -> a -> a -> a
flip_ func x z = func z x

-- • map
map_ :: (a -> a) -> [a] -> [a]
map_ _ [] = []
map_ func (x:xs) = func x : map_ func xs

-- • filter
filter_ :: (a -> Bool) -> [a] -> [a]
filter_ _ [] = []
filter_ func (x:xs)
  | func x = x : filter_ func xs
  | otherwise = filter_ func xs
