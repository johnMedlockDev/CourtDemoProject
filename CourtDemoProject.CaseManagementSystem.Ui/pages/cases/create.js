import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import styles from '../../styles/pages/cases/CreateCase.module.scss'

const CreateCasePage = () => {
	const [caseData, setCaseData] = useState({
		courtName: '',
		caseType: '' // You might want to use a select dropdown for case types
		// Add other fields as needed
	})
	const router = useRouter()

	const handleChange = (e) => {
		setCaseData({ ...caseData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/Cases', caseData)
			router.push('/cases')
		} catch (error) {
			console.error('Error creating case:', error)
		}
	}

	return (
		<div className={styles.createCase}>
			<h1>Create New Case</h1>
			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="courtName">Court Name:</label>
					<input
						type="text"
						id="courtName"
						name="courtName"
						value={caseData.courtName}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="caseType">Case Type:</label>
					<select
						id="caseType"
						name="caseType"
						value={caseData.caseType}
						onChange={handleChange}
						required
					>
						{/* Populate the options dynamically based on your case types */}
						<option value="">Select Case Type</option>
						<option value="type1">Type 1</option>
						<option value="type2">Type 2</option>
						{/* ... other case types ... */}
					</select>
				</div>
				{/* Add other input fields as needed */}
				<button type="submit">Create Case</button>
			</form>
		</div>
	)
}

export default CreateCasePage
