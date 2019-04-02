import socket
import sys

class client():
	def __init__(self, port=14988, host='localhost'):
		self.port = port
		self.server_address = (host, port)
	def signal(self, message='hahahaha'):
		try:
			#message = 'This is the message.  It will be repeated.'
			self.sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
			self.sock.connect(self.server_address)
			print (message)
			#message += '\nEXIT'
			message = str.encode(message)
			self.sock.sendall(message)
			self.sock.close()
			# Look for the response
			# amount_received = 0
			# amount_expected = len(message)

			# while True:
			# 	data = self.sock.recv(32)
			# 	amount_received += len(data)
			# 	if len(data) < 32: 
			# 		break

#MAY NEED TO DISCONECT
		finally: 
			print('Finally...')
			#self.sock.close()
	# def close(self):
	# 	self.sock.shutdown()