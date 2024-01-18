import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import styles from '../../styles/pages/charges/CreateCharge.module.scss'

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
		<div className={styles.createCharge}>
			<h1>Create New Charge</h1>
			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="chargeName">Charge Name:</label>
					<input
						type="text"
						id="chargeName"
						name="chargeName"
						value={chargeData.chargeName}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="chargeCode">Charge Code:</label>
					<input
						type="text"
						id="chargeCode"
						name="chargeCode"
						value={chargeData.chargeCode}
						onChange={handleChange}
						required
					/>
				</div>
				{/* Add other input fields as needed */}
				<button type="submit">Create Charge</button>
			</form>
		</div>
	)
}

export default CreateChargePage
