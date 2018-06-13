import hashlib

passwords = [line.rstrip("\n") for line in open('plaintext_passwords.txt')]

rainbow = open("rainbow_table.txt", "w")

for password in passwords:
	hashing = hashlib.md5(password.encode())
	rainbow.write(hashing.hexdigest() + "\n")
	print(password, ' and now hashed ', hashing)
rainbow.close()

