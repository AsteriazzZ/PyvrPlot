import numpy as np
from .vrclient import client
import json
class Grid:
	def __init__(self, arr=[]):
		self.grid_val = np.array(arr).astype(np.float32)
		self.skt = None
		self.client = None
		self.n = "GAOSHILIANG"
		self.message = {}
	def write(self, dir):
		self.grid_val.tofile(dir+'.pyvr')
	def array(self,arr):
		self.grid_val = np.array(arr).astype(np.float32)
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
		if self.grid_val is None:
			print("YOU MUST HAVE SOME DATA TO PLOT!")
			return
		if self.client is None:
			self.setup()
		self.write(self.n)
		self.message['fname'] = self.n + '.pyvr'
		self.message['time_dim'] = (self.grid_val.shape[0])
		self.message['have_time'] = (self.grid_val.shape[0] == 1)
		self.message['c_type'] = 'GRID'
		self.message['x_dim'] = (self.grid_val.shape[1])
		self.message['y_dim'] = (self.grid_val.shape[2])
		self.message['z_dim'] = -1
		self.signal(message = json.dumps(self.message))

	def on_grid(self, f, dimX=100, dimY=100, domain=(-1.,1.,-1.,1.), frames=None):
		x1,x2,y1,y2 = domain
		xx = np.linspace(x1,x2,dimX)
		yy = np.linspace(y1,y2,dimY)
		if frames is None:
			self.grid_val = np.array([f(xx[:, None] ,yy[None, :])]).astype(np.float32)
		else:
			self.grid_val = np.array([f(t,xx[:, None] ,yy[None, :]) for t in frames]).astype(np.float32)