import hashlib, re, sys

passwords = [line.rstrip("\n") for line in open('plaintext_passwords.txt')]



hashes = [line.rstrip("\n") for line in open('rainbow.txt')]

print(hashes)

