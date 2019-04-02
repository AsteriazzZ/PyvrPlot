import numpy as np
from .vrclient import client
import json
from numpy import amax, amin
from numpy.random import normal, random, randint
import os
class HeatMap:
	def __init__(self, arr):
		assert len(arr.shape) == 4
		self.hm_val = norm(np.array(arr).astype(np.float32))
		self.skt = None
		self.client = None
		self.n = "HGAOSHILIANG"
		self.message = {}
	def write(self, dir):
		self.hm_val.tofile(dir+'.pyvr')
	def array(self,arr):
		self.hm_val = np.array(arr).astype(np.float32)
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
		if self.hm_val is None:
			print("YOU MUST HAVE SOME DATA TO PLOT!")
			return
		if self.client is None:
			self.setup()
		self.write(self.n)
		self.message['fname'] = os.getcwd() + '\\' + self.n + '.pyvr'
		self.message['time_dim'] = self.hm_val.shape[0]
		self.message['have_time'] = (self.hm_val.shape[0] != 1)
		self.message['c_type'] = 'HEATMAP'
		self.message['x_dim'] = self.hm_val.shape[1]
		self.message['y_dim'] = self.hm_val.shape[2]
		self.message['z_dim'] = self.hm_val.shape[3]
		self.signal(message = json.dumps(self.message))

def gau(res):
	r = randint(0, 100)
	x = normal(random()*5+r, scale=0.7, size=res)
	y = normal(random()*5+r, scale=1.0, size=res)
	z = normal(random()*5+r, scale=1.2, size=res)
	return np.stack([x, y, z], axis=0)

def norm(arr):
	vmin = amin(arr)
	vmax = amax(arr)
	return (arr - vmin) / (vmax - vmin)

def on_box(f, dimX=100, dimY=100, dimZ=100, domain=(-1.,1.,-1.,1.,-1.,1.), frames=None):
	x1,x2,y1,y2, z1, z2 = domain
	xx = np.linspace(x1,x2,dimX)
	yy = np.linspace(y1,y2,dimY)
	zz = np.linspace(z1,z2,dimZ)
	if frames is None:
		return np.array([f(xx[:, None, None] ,yy[None, :, None], zz[None, None, :])]).astype(np.float32)
	else:
		return np.array([f(t,xx[:, None, None] ,yy[None, :, None], zz[None, None, :]) for t in frames]).astype(np.float32)

def fff(x,y,z):
	return 3. - (x**2 + y**2 + z**2)