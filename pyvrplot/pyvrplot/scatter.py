import numpy as np
from .vrclient import client
import json
from numpy import amax, amin
from numpy.random import normal, random, randint
import os
class Scatter:
	def __init__(self, arr):
		self.scatter_val = norm(np.array(arr).astype(np.float32))
		self.skt = None
		self.client = None
		self.n = "SGAOSHILIANG"
		self.message = {}
	def write(self, dir):
		self.scatter_val.T.tofile(dir+'.pyvr')
	def array(self,arr):
		self.scatter_val = np.array(arr).astype(np.float32)
	def setup(self, port=14988):
		if self.client is not None:
			return
		self.client = client(port, 'localhost')
		#self.server = server = SocketServer.TCPServer(('localhost', port), MyTCPHandler)
		#print('to be done..')
	def signal(self, message='gaoshiliangNB'):
		if self.client is None:
			print('set up your client!')
		else:
			self.client.signal(message)
	def show(self):
		if self.scatter_val is None:
			print("YOU MUST HAVE SOME DATA TO PLOT!")
			return
		if self.client is None:
			self.setup()
		self.write(self.n)
		self.message['fname'] = os.getcwd() + '\\' + self.n + '.pyvr'
		self.message['time_dim'] = -1
		self.message['have_time'] = False
		self.message['c_type'] = 'SCATTER'
		self.message['x_dim'] = -1
		self.message['y_dim'] = self.scatter_val.shape[1]
		self.message['z_dim'] = -1
		self.signal(message = json.dumps(self.message))

def gau(res):
	r = randint(0, 100)
	x = normal(random()*5+r, scale=0.7, size=res)
	y = normal(random()*5+r, scale=1.0, size=res)
	z = normal(random()*5+r, scale=1.2, size=res)
	return np.stack([x, y, z], axis=0)

def norm(arr):
	assert len(arr) == 3
	xmax = amax(arr[0])
	xmin = amin(arr[0])
	ymax = amax(arr[1])
	ymin = amin(arr[1])
	zmax = amax(arr[2])
	zmin = amin(arr[2])
	temp = max([xmax-xmin, ymax-ymin, zmax-zmin])
	arr[0] = arr[0] - xmin
	arr[1] = arr[1] - ymin
	arr[2] = arr[2] - zmin
	arr = arr / temp
	return arr