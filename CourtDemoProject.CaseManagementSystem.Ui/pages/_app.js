/* eslint-disable react/prop-types */

import Navbar from '../components/Navbar'

function App ({ Component, pageProps }) {
	return (
		<>
			<Navbar />
			<Component {...pageProps} />
		</>
	)
}

export default App
