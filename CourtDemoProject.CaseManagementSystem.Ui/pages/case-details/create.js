import { useState } from 'react'
import axios from 'axios'
import { useRouter } from 'next/router'
import styles from '../../styles/pages/case-details/CreateDetail.module.scss'

const CreateCaseDetailPage = () => {
	const [detailData, setDetailData] = useState({
		description: '',
		docketDetail: '',
		documentUri: '' // Assuming this is a URL
		// Add other fields as needed, such as dates
	})
	const router = useRouter()

	const handleChange = (e) => {
		setDetailData({ ...detailData, [e.target.name]: e.target.value })
	}

	const handleSubmit = async (e) => {
		e.preventDefault()
		try {
			await axios.post('http://api:8080/v1/CaseDetails', detailData)
			router.push('/case-details')
		} catch (error) {
			console.error('Error creating case detail:', error)
		}
	}

	return (
		<div className={styles.createDetail}>
			<h1>Create New Case Detail</h1>
			<form onSubmit={handleSubmit}>
				<div>
					<label htmlFor="description">Description:</label>
					<input
						type="text"
						id="description"
						name="description"
						value={detailData.description}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="docketDetail">Docket Detail:</label>
					<input
						type="text"
						id="docketDetail"
						name="docketDetail"
						value={detailData.docketDetail}
						onChange={handleChange}
						required
					/>
				</div>
				<div>
					<label htmlFor="documentUri">Document URI:</label>
					<input
						type="url"
						id="documentUri"
						name="documentUri"
						value={detailData.documentUri}
						onChange={handleChange}
					/>
				</div>
				{/* Add other input fields as needed */}
				<button type="submit">Create Detail</button>
			</form>
		</div>
	)
}

export default CreateCaseDetailPage
