import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, TextField, FormControl, Button, Box } from '@mui/material'

const CreateChargePage = () => {
	const [chargeData, setChargeData] = useState({
		chargeName: '',
		chargeCode: ''
		// Add other fields as needed
	})
	const router = useRouter()

	const handleChange = (e) => {
		setChargeData({ ...chargeData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/Charges', chargeData)
			router.push('/charges')
		} catch (error) {
			console.error('Error creating charge:', error)
		}
	}

	return (
		<Container>
			<Typography variant="h4" sx={{ mb: 2 }}>Create New Charge</Typography>
			<form onSubmit={handleSubmit}>
				<FormControl fullWidth margin="normal">
					<TextField
						id="chargeName"
						name="chargeName"
						label="Charge Name"
						value={chargeData.chargeName}
						onChange={handleChange}
						required
					/>
				</FormControl>
				<FormControl fullWidth margin="normal">
					<TextField
						id="chargeCode"
						name="chargeCode"
						label="Charge Code"
						value={chargeData.chargeCode}
						onChange={handleChange}
						required
					/>
				</FormControl>
				{/* Add other input fields as needed */}
				<Box sx={{ mt: 2 }}>
					<Button type="submit" variant="contained" color="primary">Create Charge</Button>
				</Box>
			</form>
		</Container>
	)
}

export default CreateChargePage
