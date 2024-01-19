import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import { Container, Typography, TextField, FormControl, Button, Box, Select, MenuItem, InputLabel } from '@mui/material'

const CreateChargePage = () => {
	const [chargeData, setChargeData] = useState({
		chargeName: '',
		chargeCode: '',
		chargeType: '', // Enum value
		judgementType: '', // Enum value
		fineAmount: 0,
		sentenceLengthIndays: 0
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
				{/* Add other fields as per the ChargeEntity */}
				{/* Replace the below placeholders with actual enum values */}
				<FormControl fullWidth margin="normal">
					<InputLabel id="chargeType-label">Charge Type</InputLabel>
					<Select
						labelId="chargeType-label"
						id="chargeType"
						name="chargeType"
						value={chargeData.chargeType}
						label="Charge Type"
						onChange={handleChange}
					>
						<MenuItem value="Type1">Type 1</MenuItem>
						<MenuItem value="Type2">Type 2</MenuItem>
						{/* ... other charge types ... */}
					</Select>
				</FormControl>
				<FormControl fullWidth margin="normal">
					<InputLabel id="judgementType-label">Judgement Type</InputLabel>
					<Select
						labelId="judgementType-label"
						id="judgementType"
						name="judgementType"
						value={chargeData.judgementType}
						label="Judgement Type"
						onChange={handleChange}
					>
						<MenuItem value="Type1">Type 1</MenuItem>
						<MenuItem value="Type2">Type 2</MenuItem>
						{/* ... other judgement types ... */}
					</Select>
				</FormControl>
				<FormControl fullWidth margin="normal">
					<TextField
						id="fineAmount"
						name="fineAmount"
						label="Fine Amount"
						type="number"
						value={chargeData.fineAmount}
						onChange={handleChange}
					/>
				</FormControl>
				<FormControl fullWidth margin="normal">
					<TextField
						id="sentenceLengthIndays"
						name="sentenceLengthIndays"
						label="Sentence Length In Days"
						type="number"
						value={chargeData.sentenceLengthIndays}
						onChange={handleChange}
					/>
				</FormControl>
				<Box sx={{ mt: 2 }}>
					<Button type="submit" variant="contained" color="primary">Create Charge</Button>
				</Box>
			</form>
		</Container>
	)
}

export default CreateChargePage
